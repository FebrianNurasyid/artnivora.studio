import { fromJS} from 'immutable';
import { bindActionCreators } from 'redux';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import blankImage from '../images/blankimg.png';
import fetchUserWithIdAction from '../../store/users/actions/fetchUserWithId';
import {
    uploadProfilePicture,
    saveUserProfile,
    getParticipantProfile,
    fetchProfileImage,
} from '../../store/users/actions/editProfile';
import styles from './Styles.css';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import DatePicker from 'react-datepicker';
import { Link } from 'react-router-dom';
import "react-datepicker/dist/react-datepicker.css";
import { FaRegCalendarAlt } from "react-icons/fa";
import { toast } from 'react-toastify';
import { fileSizeLimit, fileFormatAllowed } from '../../constants/fileConstants';

class ParticipantProfile extends Component {
    state = {
        postUpdate: false,
        componentUpdated: false,
        pictureUpdating: false,
        fields: fromJS({
            salutation: 'Mevr',
            firstname: '',
            initial:'',
            lastname: '',
            insertion: '',
            maidenname:'',
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
            country: ''
        }),
        profileState: '',
        errors: fromJS({ })
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
        this.props.getParticipantProfile();
    }

    DateCustomInput = ({ value, onClick }) => (
        <div className="input-group mb-3">
            <input type="text" className="form-control" onClick={onClick} value={value} style={{ borderRight:"none" }} />
            <div className="input-group-append">
                <span className="input-group-text" style={{ backgroundColor:"#FFFFFF" }}><FaRegCalendarAlt /></span>
            </div>
        </div>
    );
    handleChange(field, value) {
        
        this.setState({
            fields: this.state.fields.set(field, value),
            errors: this.state.errors.delete(field),
        });
    }
    handleDateChange = (d) => {
        this.setState({
            fields: this.state.fields.set('birthdate', d),
            errors: this.state.errors.delete('birthdate')
        });
    }
    handleDropdownChange(e) {
        const name = e.target.name;
        const value = e.target.value;

        this.setState({
            fields: this.state.fields.set(name, value),
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
        const requiredField = ['initial', 'lastname','birthdate', 'healthcareproviderid', 'street', 'housenumber', 'zipcode', 'city', 'phonenumber', 'email'];
        let isValid = true;
        requiredField.forEach(x => {
            
            if (this.state.fields.get(x) === null || this.state.fields.get(x) === '') {
                isValid = false;
                return;
            }
        });
        return isValid;
    }
    handleSubmit = (e) => {
        e.preventDefault();
        if (!this.validateField()) {
            toast.error("You need to fill all required field", {
                position: toast.POSITION.TOP_CENTER
            });
            return;
        }

        this.props.saveUserProfile(this.state.fields.toJS());
        this.setState({
            postUpdate: false
        });
    }
    componentDidUpdate()
    {
        const { userParticipantData, userProfileData } = this.props;
        if (!this.state.postUpdate && userProfileData !== undefined) {
            this.setState({
                fields: userProfileData,
                postUpdate: true
            });
        }
        if (userParticipantData !== undefined && !this.state.componentUpdated) {
            
            this.setState({
                fields: userParticipantData,
                componentUpdated : true
            });
        }
    }

    render() {
        const { fields, errors } = this.state;
        const { userProfilePicture } = this.props;

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
                        <h3>Select Photo</h3>
                    </div>
                </div>
                <div className="form-group row">
                    <div>
                        <span className="btn btn-file  purple-button d-inline-block">
                            Browse <input type="file" onChange={this.onFileChange} />
                        </span>
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label">Salutation</label>
                    <select name={'salutation'} value={fields.get('salutation')}
                        onChange={(value) => this.handleDropdownChange(value)} className="col-sm-2 col-form-label control-label">
                        <option value="Dhr">Dhr.</option>
                        <option value="Mevr">Mevr.</option>
                    </select>
                    <div className="col-md-6">
                        <TextInputField field={'firstname'}
                            value={fields.get('firstname')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('firstname')} />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="initial_field">Initial</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField
                            field={'initial'}
                            value={fields.get('initial')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('initial')}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label">Lastname</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField
                            field={'lastname'}
                            value={fields.get('lastname')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('lastname')}
                        />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label" htmlFor="insertion_field">Insertion</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField
                            field={'insertion'}
                            value={fields.get('insertion')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('insertion')}
                        />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label" htmlFor="maidenname_field">Maiden name (if applicable)</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField
                            field={'maidenname'}
                            value={fields.get('maidenname')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('maidenname')}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label">Gender</label>
                    <div className="col-md-2" />
                    <div className="col-md-6 ">
                        <select name={'gender'} value={fields.get('gender')}
                            onChange={(value) => this.handleDropdownChange(value)} className="form-control">
                            <option value="Vrouw">Vrouw</option>
                            <option value="Man">Man</option>
                        </select></div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="birthdate_field">Birthdate</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <DatePicker className="form-control"
                            selected={fields.get('birthdate')}
                            field={'birthdate'}
                            onChange={(date) => this.handleDateChange(date)}
                            dateFormat="dd/MM/yyyy"
                            showYearDropdown
                            customInput={<this.DateCustomInput />}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="healthcare_field">Healthcare ID</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField className="form-control"
                            field={'healthcareproviderid'}
                            value={fields.get('healthcareproviderid')}
                            onChange={(field,value) => this.handleChange(field,value)}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="street_field">Street</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField
                            field={'street'}
                            value={fields.get('street')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('street')}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="housenumber_field">House number</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField
                            field={'housenumber'}
                            value={fields.get('housenumber')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('housenumber')}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="zipcode_field">Postal code</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField
                            field={'zipcode'}
                            value={fields.get('zipcode')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('zipcode')}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="city_field">City</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField
                            field={'city'}
                            value={fields.get('city')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('city')}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label">Phone Number</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField
                            field={'phonenumber'}
                            value={fields.get('phonenumber')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('phonenumber')}
                        />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label" htmlFor="phonenumbermobile_field">Mobile Phone Number</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField
                            field={'phonenumbermobile'}
                            value={fields.get('phonenumbermobile')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('phonenumbermobile')}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="email_field">Email</label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <TextInputField
                            field={'email'}
                            value={fields.get('email')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('email')}
                        />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label"></label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <label>You agree with the&nbsp;
                            <Link to="/users/privacy-statement" target="_blank">Privacy Statement</Link>
                        </label>
                    </div>
                </div>
                <div className="form-group row" >
                    <label className="col-sm-2 col-form-label"></label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <div className="custom-control custom-checkbox">
                            <input type="checkbox" className="custom-control-input" id='telephonelistagree' name={'telephonelistagree'} field={'telephonelistagree'} value={fields.get('telephonelistagree')} onChange={(value) => this.handleCheckboxChange(value)} hasError={errors.has('telephonelistagree')} />
                            <label className="custom-control-label" htmlFor="telephonelistagree">My telephone number may be mentioned on the list of participants.<span className="checkmark"></span></label>
                        </div>
                    </div>
                </div>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label"></label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
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
                    <label className="col-sm-2 col-form-label"></label>
                    <div className="col-md-2" />
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
                
            </div>);
    }
}
function mapStateToProps(state) {
    return {
        routingLocation: routingLocationSelector(state),
        loggedInUser: state.auth.loggedInUser,
        userProfilePicture: state.users.userProfilePicture,
        userProfileData: state.users.userProfileData,
        userParticipantData: state.users.userParticipantData
    }
}
export default connect(mapStateToProps, dispatch =>
    bindActionCreators({
        fetchUserWithId: fetchUserWithIdAction,
        uploadProfilePicture, saveUserProfile,
        getParticipantProfile,
        fetchProfileImage,
    }, dispatch))(ParticipantProfile);