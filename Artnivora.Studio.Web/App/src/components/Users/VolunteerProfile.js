import { fromJS } from 'immutable';
import { bindActionCreators } from 'redux';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import styles from './Styles.css';
import blankImage from '../images/blankimg.png';
import fetchUserWithIdAction from '../../store/users/actions/fetchUserWithId';
import {
    uploadProfilePicture,
    saveVolunteerProfile,
    getVolunteerProfile,
    fetchProfileImage,
} from '../../store/users/actions/editProfile';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";
import { FaRegCalendarAlt } from "react-icons/fa";
import { toast } from 'react-toastify';
import { fileSizeLimit, fileFormatAllowed } from '../../constants/fileConstants';

class VolunteerProfile extends Component {
    state = {
        postUpdate: false,
        componentUpdated: false,
        pictureUpdating: false,
        fields: fromJS({
            salutation: 'Mevr',
            firstname: '',
            initial: '',
            lastname: '',
            insertion: '',
            maidenname: '',
            gender: 'Vrouw',
            birthdate: '',
            healthcareproviderid: '',
            street: '',
            housenumber: '',
            zipcode: '',
            city: '',
            phonenumber: '',
            phonenumbermobile: '',
            email: '',
            telephonelistagree: '',
            holidayweekagree: '',
            newsletter: '',
            country:''
        }),
        profileState: '',
        errors: fromJS({})
    }

    componentWillMount() {
        this.props.fetchProfileImage();
    }

    componentDidMount() {
        const {
            routingLocation
        } = this.props;
        if (routingLocation && routingLocation.query) {
            const userId = routingLocation.query['id'];
            if (userId) {
                this.props.fetchUserWithId(userId);
            }
        }
        this.props.getVolunteerProfile();
    }

    handleDropdownChange(e) {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({
            fields: this.state.fields.set(name, value),
        });
    }

    handleChange(field, value) {
        this.setState({
            fields: this.state.fields.set(field, value),
            errors: this.state.errors.delete(field),
        });
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
            fields: this.state.fields.set('birthdate', d),
            errors: this.state.errors.delete('birthdate')
        });
    }

    handleCheckboxChange(e) {
        const name = e.target.name;
        const value = e.target.checked;

        this.setState({
            fields: this.state.fields.set(name, value),
            errors: this.state.errors.delete(name),
        });
    }

    onFileChange = (e) => {
        const files = e.target.files;

        let formData = new FormData();
        let fileType = '';

        if (files[0] !== null) {
            fileType = files[0].type;
        }

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
        this.setState({
            pictureUpdating: true
        });
        this.props.uploadProfilePicture(formData);
    }

    validateField = () => {
        const requiredField = ['firstname', 'lastname', 'birthdate', 'contactperson1', 'contactpersonphone1', 'street', 'housenumber', 'zipcode', 'phonenumber', 'email', 'country'];
        let isValid = true;
        requiredField.forEach(x => {

            if (this.state.fields.get(x) === null || this.state.fields.get(x) === '') {
                isValid = false;
                return;
            }
        });
        return isValid;
    }

    handleSubmit = (e) =>{
        e.preventDefault();
        
        if (!this.validateField()) {
            toast.error("You need to fill all required field", {
                position: toast.POSITION.TOP_CENTER
            });
            return;
        }
        this.props.saveVolunteerProfile(this.state.fields.toJS());
        this.setState({
            postUpdate: false
        });
    }

    componentDidUpdate() {
        const { userVolunteerData, volunteerUpdatedData } = this.props;
        
        if (!this.state.postUpdate && volunteerUpdatedData !== undefined) {
            this.setState({
                fields: volunteerUpdatedData,
                postUpdate: true
            });
        }

        if (userVolunteerData !== undefined && !this.state.componentUpdated) {

            this.setState({
                fields: userVolunteerData,
                componentUpdated: true
            });
        }
    }

    render() {
        const { userProfilePicture } = this.props;
        const { fields, errors } = this.state;

        return (
            <div className="container">
                <div className=" form-group row">
                    <div className="col-md-12">
                        <h1>Personal Data</h1>
                    </div>
                    <div className="col-md-12">
                        <img alt="profile picture" width="300px" src={userProfilePicture || blankImage} />
                    </div>
                    <div className="col-md-12">
                        <h3>Select foto</h3>
                    </div>
                </div>
                <div className="form-group row">
                    <div className="col-md-12">
                        <span className="btn btn-file btn-file-upload purple-button">
                            Browse <input type="file" onChange={this.onFileChange} style={{ position: 'relative !important' }} />
                         </span>
                    </div>
                </div>
                <hr className="style14" />
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label">Salutation</label>
                    <div className="col-md-4">
                        <select name={'salutation'} value={fields.get('salutation')}
                            onChange={(value) => this.handleDropdownChange(value)} className="col-md-4 col-form-label control-label">
                            <option value="Dhr">Dhr.</option>
                            <option value="Mevr">Mevr.</option>
                        </select>
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="firstname_field">First Name(s)</label>
                    <div className="col-md-3">
                        <TextInputField field={'firstname'}
                            value={fields.get('firstname')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('firstname')} />
                    </div>
                    <div className="col-md-2" />
                    <label className="col-sm-2 col-form-label" htmlFor="insertion_field">Insertion</label>
                    <div className="col-md-3">
                        <TextInputField field={'insertion'}
                            value={fields.get('insertion')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('insertion')} />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="lastname_field">Last Name</label>
                    <div className="col-md-3">
                        <TextInputField field={'lastname'}
                            value={fields.get('lastname')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('lastname')} />
                    </div>
                    <div className="col-md-2" />
                    <label className="col-sm-2 col-form-label" htmlFor="maiden_field">Maiden Name (if applicable)</label>
                    <div className="col-md-3">
                        <TextInputField field={'maidenname'}
                            value={fields.get('maidenname')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('maidenname')} />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="lastnamepartner_field">LastName Partner</label>
                    <div className="col-md-3">
                        <TextInputField field={'lastnamepartner'}
                            value={fields.get('lastnamePartner')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('lastnamePartner')} />
                    </div>
                    <div className="col-md-2" />
                    <label className="col-sm-2 col-form-label" htmlFor="maritalstatus_field">Marital Status</label>
                    <div className="col-md-3">
                        <select name={'maritalstatus'} value={fields.get('maritalstatus')}
                            onChange={(value) => this.handleDropdownChange(value)} className="form-control">
                            <option value="1">Married</option>
                            <option value="2">InRelation</option>
                            <option value="3">Single</option>
                            <option value="9">Other</option>
                        </select>
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="birthdate_field">Birthdate</label>
                    <div className="col-md-3">
                        <DatePicker className="form-control"
                            selected={fields.get('birthdate')}
                            field={'birthdate'}
                            onChange={(date) => this.handleDateChange(date)}
                            dateFormat="MMMM d, yyyy"
                            showYearDropdown
                            customInput={<this.DateCustomInput />}
                        />
                    </div>
                    <div className="col-md-2" />
                    <label className="col-sm-2 col-form-label control-label" htmlFor="country_field">Country</label>
                    <div className="col-md-3">
                        <TextInputField field={'country'}
                            value={fields.get('country')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('country')} />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="street_field">Street</label>
                    <div className="col-md-3">
                        <TextInputField field={'street'}
                            value={fields.get('street')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('street')} />
                    </div>
                    <div className="col-md-2" />
                    <label className="col-sm-2 col-form-label control-label" htmlFor="housenumber_field">House Number</label>
                    <div className="col-md-3">
                        <TextInputField field={'housenumber'}
                            value={fields.get('housenumber')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('housenumber')} />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="zipcode_field">Postal Code</label>
                    <div className="col-md-3">
                        <TextInputField field={'zipcode'}
                            value={fields.get('zipcode')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('zipcode')} />
                    </div>
                    <div className="col-md-2" />
                    <label className="col-sm-2 col-form-label control-label" htmlFor="residence_field">Residence</label>
                    <div className="col-md-3">
                        <TextInputField field={'residence'}
                            value={fields.get('residence')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('residence')} />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label">Gender</label>
                    <div className="col-md-4">
                        <select name={'gender'} value={fields.get('gender')}
                            onChange={(value) => this.handleDropdownChange(value)} className="col-md-4 col-form-label control-label">
                            <option value="Vrouw">Vrouw</option>
                            <option value="Man">Man</option>
                        </select>
                    </div>
                </div>
                <hr className="style14" />
                <div className=" form-group row">
                    <div className="col-md-12">
                        <h1>Contact Details</h1>
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="phonenumber_field">Phone Number</label>
                    <div className="col-md-3">
                        <TextInputField field={'phonenumber'}
                            value={fields.get('phonenumber')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('phonenumber')} />
                    </div>
                    <div className="col-md-2" />
                    <label className="col-sm-2 col-form-label control-label" htmlFor="email_field">Email</label>
                    <div className="col-md-3">
                        <TextInputField field={'email'}
                            value={fields.get('email')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('email')} />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label" htmlFor="phonenumbermobile_field">Mobile Number</label>
                    <div className="col-md-3">
                        <TextInputField field={'phonenumbermobile'}
                            value={fields.get('phonenumbermobile')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('phonenumbermobile')} />
                    </div>
                </div>
                <hr className="style14" />
                <div className=" form-group row">
                    <div className="col-md-12">
                        <h1>Contacts</h1>
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="contactperson1_field">Contact Person 1</label>
                    <div className="col-md-3">
                        <TextInputField field={'contactperson1'}
                            value={fields.get('contactperson1')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('contactperson1')} />
                    </div>
                    <div className="col-md-2" />
                    <label className="col-sm-2 col-form-label control-label" htmlFor="contactpersonphone1_field">Phone Number</label>
                    <div className="col-md-3">
                        <TextInputField field={'contactpersonphone1'}
                            value={fields.get('contactpersonphone1')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('phoneNumberContact1')} />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label" htmlFor="contactperson2_field">Contact Person 2</label>
                    <div className="col-md-3">
                        <TextInputField field={'contactperson2'}
                            value={fields.get('contactperson2')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('contactperson2')} />
                    </div>
                    <div className="col-md-2" />
                    <label className="col-sm-2 col-form-label" htmlFor="contactpersonphone2_field">Phone Number</label>
                    <div className="col-md-3">
                        <TextInputField field={'contactpersonphone2'}
                            value={fields.get('contactpersonphone2')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('contactpersonphone2')} />
                    </div>
                </div>
                <hr className="style14" />
                <div className="form-group row">                    
                    <div className="col-md-10">
                        <div className="custom-control custom-checkbox">
                            <input type="checkbox" className="custom-control-input" id="holidayweekagree" name={'holidayweekagree'} field={'holidayweekagree'} value={fields.get('holidayweekagree')} onChange={(value) => this.handleCheckboxChange(value)} hasError={errors.has('holidayweekagree')} />
                            <label className="custom-control-label" htmlFor="holidayweekagree">I declare that I have filled in the above truthfully and authorize the VOOR ELKAAR holiday weeks to store this data in a digital file and use it for the holiday week<span className="checkmark"></span></label>
                        </div>
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
                    <div className="col-md-6">
                        <div className="custom-control custom-checkbox">
                            <input
                                className="btn pink-button"
                                type="button"
                                value="Save"
                                onClick={this.handleSubmit}                                
                            />
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

function mapStateToProps(state) {
    return {
        routingLocation: routingLocationSelector(state),
        loggedInUser: state.auth.loggedInUser,
        userProfilePicture: state.users.userProfilePicture,
        userVolunteerData: state.users.userVolunteerData,
        volunteerUpdatedData: state.users.volunteerUpdatedData
    }
}
export default connect(mapStateToProps, dispatch =>
    bindActionCreators({
        fetchUserWithId: fetchUserWithIdAction,
        uploadProfilePicture,
        saveVolunteerProfile,
        getVolunteerProfile,
        fetchProfileImage,
    }, dispatch))(VolunteerProfile);