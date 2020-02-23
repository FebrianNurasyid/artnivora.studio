﻿import { fromJS } from 'immutable';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import clearUserWithIdAction from '../../store/users/actions/clearUserWithId';
import fetchUserWithIdAction from '../../store/users/actions/fetchUserWithId';
import registerUserAction from '../../store/users/actions/registerUser';
import { userToEditSelector } from '../../store/users/selectors/userSelectors';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import styles from './Styles.css';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';
import { attachmentProductionAction } from '../../store/productions/actions/attachmentProduction';
import { fileFormatAllowed, fileSizeLimit } from '../../constants/fileConstants';
import fetchProductionAttachmentWithId from '../../store/productions/actions/fetchProductionAttachmentWithId';
import { saveProduction } from '../../store/productions/actions/saveProduction';
import { toast } from 'react-toastify';

class AddOrEdit extends Component {

    constructor(props) {
        super(props);
        this.state = {
            fields: fromJS({
                title: '',
                category: 'Logo',
                themes: 'Cartoon',
                concept: '',
                status: 'Packaging',
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
    }

    componentDidMount() {
        const {
            routingLocation
        } = this.props;
        this.props.clearUserWithId();
        if (routingLocation && routingLocation.query) {
            const userId = routingLocation.query['id'];
            if (userId) {
                this.props.fetchUserWithId(userId);
            }
        }
    }

    componentDidUpdate(prevProps) {
        const { userToEdit } = this.props;
        if (userToEdit && !prevProps.userToEdit) {
            this.setState({
                fields: fromJS({
                    divisionname: '',
                }),
                errors: fromJS({}),
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

    handleSubmit() {
        const { fields, attachment } = this.state;

        this.setState({
            errors: fromJS({}),
        });

        const unfilledValues = fields.filter((value) => {
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
                this.props.history.push('/production');
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
        const { attachmentProduction } = this.props;
        this.props.fetchProductionAttachmentWithId(attachmentProduction);
        e.preventDefault();
    }

    render() {
        const editMode = this.props.userToEdit != null;
        const {
            fields,
            errors,
            attachment,
        } = this.state;

        return (
            <span>
                {!editMode && <h3> New Production </h3>}
                {editMode && <h2> Edit Production </h2>}
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

                    <label className="col-sm-2 col-form-label control-label" htmlFor="divisionname_field">Concept</label>
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
                            extraClassName="btn-cancel"
                            type="button"
                            onClick={this.props.history.goBack}
                        >Cancel</HVBButton>
                    </div>
                </div>
            </span>
        );
    }
}

export default connect(
    (state) => {
        return {
            userToEdit: userToEditSelector(state),
            routingLocation: routingLocationSelector(state),
            attachmentProduction: state.productions.attachmentProduction
        }
    },
    dispatch => bindActionCreators({
        registerUser: registerUserAction,
        fetchUserWithId: fetchUserWithIdAction,
        clearUserWithId: clearUserWithIdAction,
        attachmentProductionAction,
        fetchProductionAttachmentWithId,
        saveProduction,
    }, dispatch),
)(AddOrEdit);