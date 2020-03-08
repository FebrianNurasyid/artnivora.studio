import { fromJS } from 'immutable';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import clearProdWithIdAction from '../../store/productions/actions/clearProdWithId';
import clearAttachmentAction from '../../store/productions/actions/clearAttachment';
import fetchUserWithIdAction from '../../store/productions/actions/fetchProductionWithId';
import registerUserAction from '../../store/users/actions/registerUser';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import styles from './Styles.css';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';
import { attachmentProductionAction } from '../../store/productions/actions/attachmentProduction';
import { fileFormatAllowed, fileSizeLimit } from '../../constants/fileConstants';
import fetchProductionAttachmentWithId from '../../store/productions/actions/fetchProductionAttachmentWithId';
import { saveProduction } from '../../store/productions/actions/saveProduction';
import { toast } from 'react-toastify';
import { prodToEditSelector } from '../../store/productions/selectors/productionsSelectors';

class EditKeywording extends Component {

    constructor(props) {
        super(props);
        this.state = {
            fields: fromJS({
                id: '',
                title: '',
                category: '',
                themes: '',
                concept: '',
                status: '',
                remark: '',
                uploadedstatus: '',
            }),
            attachment: fromJS({
                id: '',
                filename: '',
                filepath: '',
            }),
            errors: fromJS({}),
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onAttachmentChangeHandler = this.onAttachmentChangeHandler.bind(this);
        this.handleActionDownload = this.handleActionDownload.bind(this);
        this.handleRevise = this.handleRevise.bind(this);
    }

    componentDidMount() {
        const {
            routingLocation
        } = this.props;
        this.props.clearProdWithId();
        if (routingLocation && routingLocation.query) {
            const task = routingLocation.query['task'];
            if (task) {
                this.setState({
                    taskpage: this.state.taskpage.set('task', task),
                });
            }
            const userId = routingLocation.query['id'];
            if (userId) {
                this.props.fetchProdWithId(userId);
            }
        }
    }

    componentDidUpdate(prevProps) {
        const { prodToEdit } = this.props;
        if (prodToEdit && !prevProps.prodToEdit) {
            this.setState({
                fields: fromJS({
                    id: prodToEdit.id,
                    title: prodToEdit.title,
                    category: prodToEdit.category,
                    themes: prodToEdit.themes,
                    concept: prodToEdit.concept,
                    status: prodToEdit.status,
                    remark: prodToEdit.remark,
                    uploadedstatus: '',
                }),
                errors: fromJS({}),
                attachment: fromJS({
                    id: prodToEdit.attacment.productionAttachementId,
                    filename: prodToEdit.attacment.fileName,
                    filepath: prodToEdit.attacment.filePath,
                }),
            })
        }
        const { attachmentProduction } = this.props;
        if (attachmentProduction && attachmentProduction !== prevProps.attachmentProduction) {
            this.setState({
                attachment: fromJS({
                    id: attachmentProduction.id,
                    filename: attachmentProduction.fileName,
                    filepath: attachmentProduction.path,
                }),
            })
        }
    }

    handleChange(field, value) {
        this.setState({
            fields: this.state.fields.set(field, value),
            errors: this.state.errors.delete(field),
        });
    }

    handleDropdownChange(e) {
        const name = e.target.name;
        const value = e.target.value;

        this.setState({
            fields: this.state.fields.set(name, value),
        });
    }

    handleRevise() {       
        this.handleSave('Packaging');
    }

    handleSubmit() {        
        this.handleSave('Uploading');
    }

    handleSave(status) {
        const { attachment } = this.state;
        var oFfields = this.state.fields;
        var fields = oFfields.map((value, field) => {
            if (field === 'status') {
                var regexValue = status
                return regexValue;
            }
            else {
                return value;
            }
        });
        this.setState({ fields: fields });
        
        this.setState({
            errors: fromJS({}),
        });

        const nonMandatoryFields = fields.filter((v, f) => {
            return (f !== 'id' && f !== 'uploadedstatus' && f !== 'remark')
        })

        const unfilledValues = nonMandatoryFields.filter((value) => {
            return value.trim() === '';
        });

        const unfilledAttahcment = attachment.filter((value) => {
            return value.trim() === '';
        });

        const hasFilledAllValues = unfilledValues.count() === 0;
        const hasAttachment = unfilledAttahcment.count() === 0;

        if (hasFilledAllValues) {
            if (!hasAttachment) {
                toast.error("Please attach the file...", {
                    position: toast.POSITION.TOP_CENTER
                });
            }
            else {
                this.props.saveProduction(fields.toJS(), attachment.toJS());
                this.props.clearAttachment();
                this.props.history.push('/production/keywording');
            }
        } else {
            let errors = fromJS({});

            unfilledValues.map((value, field) => {
                errors = errors.set(field, true);
            });

            this.setState({
                errors: this.state.errors.merge(errors),
            });
        }
    }

    onAttachmentChangeHandler = e => {
        const files = e.target.files;
        if (files.length > 0) {
            let formData = new FormData();
            let fileType = '';

            if (files[0] !== null)
                fileType = files[0].type;

            if (files[0].size > fileSizeLimit) {
                toast.error("This file size is not allowed", {
                    position: toast.POSITION.TOP_CENTER
                });
                return;
            }
            if (fileFormatAllowed.indexOf(fileType) == -1) {
                toast.error("This file format is not allowed", {
                    position: toast.POSITION.TOP_CENTER
                });
                return;
            }

            formData.append('file', files[0]);
            this.props.attachmentProductionAction(formData);
        }
    }

    handleActionDownload = (e) => {
        e.preventDefault();
        const { attachmentProduction } = this.props;
        if (!attachmentProduction) {
            const { attachment } = this.state;
            console.log(attachment);
            this.props.fetchProductionAttachmentWithId(attachment.toJS(), 'real');
        }
        else
            this.props.fetchProductionAttachmentWithId(attachmentProduction, 'temp');
    }

    render() {
        const editMode = this.props.prodToEdit != null;
        const {
            fields,
            errors,
            attachment,
            taskpage,
        } = this.state;

        return (
            <span>
                {editMode && <h3> Edit Keywording </h3>}
                <hr className="style12" />
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="title_field">Title</label>
                    <div className="col-md-4">
                        <TextInputField
                            field={'title'}
                            value={fields.get('title')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('title')}
                        />
                    </div>

                    <label className="col-sm-2 col-form-label control-label" htmlFor="category_field">Category</label>
                    <div className="col-md-4">
                        <select name={'category'} value={fields.get('category')}
                            onChange={(value) => this.handleDropdownChange(value)}
                            className="col-sm-12 col-form-label control-label">
                            <option value="Logo">Logo</option>
                            <option value="Illustration">Illustration</option>
                        </select>
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="themes_field">Themes</label>
                    <div className="col-md-4">
                        <select name={'themes'} value={fields.get('themes')}
                            onChange={(value) => this.handleDropdownChange(value)}
                            className="col-sm-12 col-form-label control-label">
                            <option value="Cartoon">Cartoon</option>
                            <option value="Anime">Anime</option>
                        </select>
                    </div>

                    <label className="col-sm-2 col-form-label control-label" htmlFor="concept_field">Concept</label>
                    <div className="col-md-4">
                        <TextInputField
                            field={'concept'}
                            value={fields.get('concept')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('concept')}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="concept_field">Remark</label>
                    <div className="col-md-4">
                        <TextInputField
                            field={'remark'}
                            value={fields.get('remark')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('remark')}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="divisionname_field">Attachment File</label>
                    <div className="col-md-4">
                        <span className="btn btn-file attachment-button">
                            Attach File &nbsp; <i className="fa fa-paperclip" aria-hidden="true"></i> <input type="file" name="file" onChange={this.onAttachmentChangeHandler} />
                        </span>
                        <a onClick={this.handleActionDownload} href="#"> {`${attachment.get('filename')}`} </a>
                    </div>
                </div>
                {
                    errors.valueSeq().map((error) => {
                        if (typeof (error) === "string") {
                            return (
                                <div key={`error-${error}`} className={styles.error}>
                                    {error}
                                </div>
                            );
                        }
                    })
                }
                <hr className="style12" />
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label"></label>
                    <div className="col-md-6">
                        <HVBButton
                            className="btn btn-primary"
                            type="button"
                            onClick={this.handleSubmit}
                        >Submit</HVBButton>
                        <HVBButton
                            className="btn btn-primary"
                            type="button"
                            extraClassName="btn-cancel"
                            onClick={this.handleRevise}
                        >Revise</HVBButton>
                        <HVBButton
                            className="btn btn-primary"
                            extraClassName="btn-cancel"
                            type="button"
                            onClick={this.props.history.goBack}
                        >Cancel</HVBButton>
                    </div>
                </div>
                <hr className="style12" />
            </span>
        );
    }
}

export default connect(
    (state) => {
        return {
            routingLocation: routingLocationSelector(state),
            attachmentProduction: state.productions.attachmentProduction,
            prodToEdit: prodToEditSelector(state),
        }
    },
    dispatch => bindActionCreators({
        registerUser: registerUserAction,
        clearProdWithId: clearProdWithIdAction,
        attachmentProductionAction,
        fetchProductionAttachmentWithId,
        saveProduction,
        fetchProdWithId: fetchUserWithIdAction,
        clearAttachment: clearAttachmentAction,
    }, dispatch),
)(EditKeywording);
