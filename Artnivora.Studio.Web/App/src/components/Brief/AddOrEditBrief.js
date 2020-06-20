import { fromJS } from 'immutable';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";
import { FaRegCalendarAlt } from "react-icons/fa";

class AddOrEditBrief extends Component {

    constructor(props) {
        super(props);
        this.state = {
            fields: fromJS({
                id: '',
                descriptions: '',
                category: 'Logo',
                themes: 'Cartoon',
                concept: '',
                status: 'Packaging',
                remark: '',
                uploadedstatus: '',
                dateofbrief: '',
            }),
            attachment: fromJS({
                id: '',
                filename: '',
                filepath: '',
            }),
            errors: fromJS({}),
        };
    }

    DateCustomInput = ({ value, onClick }) => (
        <div className="input-group mb-3">
            <input type="text" className="form-control" onClick={onClick} value={value} style={{ borderRight: "none" }} />
            <div className="input-group-append">
                <span className="input-group-text" style={{ backgroundColor: "#FFFFFF" }}><FaRegCalendarAlt /></span>
            </div>
        </div>
    );

    handleDateChange = (d) => {
        this.setState({
            fields: this.state.fields.set('dateofbrief', d),
            errors: this.state.errors.delete('dateofbrief')
        });
    }

    handleChange(field, value) {
        this.setState({
            fields: this.state.fields.set(field, value),
            errors: this.state.errors.delete(field),
        });
    }

    render() {
        const editMode = this.props.briefToEdit != null;
        const {
            fields,
            errors,
            attachment,
            taskpage,
        } = this.state;

        return (
            <span>
                {!editMode && <h3> New Brief </h3>}
                {editMode && <h3> Edit Brief </h3>}
                <hr className="style12" />
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="title_field">Date Of Brief</label>
                    <div className="col-md-4">
                        <DatePicker
                            className="col-md-4"
                            selected={fields.get('dateofbrief')}
                            field={'dateofbrief'}
                            onChange={(date) => this.handleDateChange(date)}
                            dateFormat="MMMM d, yyyy"
                            showYearDropdown
                            customInput={<this.DateCustomInput />}
                        />
                    </div>
                </div>

                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="title_field">Descriptions</label>
                    <div className="col-md-10">
                        <textarea
                            className="form-control"
                            rows='4'
                            cols='10'
                            field={'descriptions'}
                            value={fields.get('descriptions')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('descriptions')}
                        />
                    </div>
                </div>

                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="title_field">Industry</label>
                    <div className="col-md-4">
                        <TextInputField
                            field={'descriptions'}
                            value={fields.get('descriptions')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('descriptions')}
                        />
                    </div>
                </div>

                <hr className="style12" />
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label"></label>
                    <div className="col-md-6">
                        <HVBButton

                            type="button"
                            onClick={this.handleSubmit}
                        >Save</HVBButton>

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
            //routingLocation: routingLocationSelector(state),
            //attachmentProduction: state.productions.attachmentProduction,
            //prodToEdit: prodToEditSelector(state),
        }
    },
    dispatch => bindActionCreators({
        //registerUser: registerUserAction,
        //clearProdWithId: clearProdWithIdAction,
        //attachmentProductionAction,
        //fetchProductionAttachmentWithId,
        //saveProduction,
        //fetchProdWithId: fetchUserWithIdAction,
        //clearAttachment: clearAttachmentAction,
    }, dispatch),
)(AddOrEditBrief);
