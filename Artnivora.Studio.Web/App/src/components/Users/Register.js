import { fromJS } from 'immutable';
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
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';

class Register extends Component {

    constructor(props) {
        super(props);
        this.state = {
            fields: fromJS({
                salutation: 'Mevr',
                firstname: '',
                lastname: '',
                insertion: '',
                street: '',
                housenumber: '',
                zipcode: '',
                city: '',
                phonenumber: '',
                phonenumbermobile: '',
                email: '',
                iagree: '',
                newsletter: '',
            }),
            errors: fromJS({})
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
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
                    username: userToEdit.username,
                    lastName: userToEdit.lastName,
                    email: userToEdit.email,
                    password: 'secret',
                    repeat_password: 'secret',
                }),
                errors: fromJS({}),
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

    handleCheckboxChange(e) {
        const name = e.target.name;
        const value = e.target.checked;

        this.setState({
            fields: this.state.fields.set(name, value),
            errors: this.state.errors.delete(name),
        });
    }

    handleSubmit() {
        const { fields } = this.state;

        this.setState({
            errors: fromJS({}),
        });

        const simpleEmailRegexp = new RegExp('[^@]+@[^\.]+\..+');
        const nonMandatoryFields = fields.filter((v, f) => {
            return (f !== 'insertion' && f != 'phonenumbermobile' && f != 'newsletter')
        })
        const unfilledValues = nonMandatoryFields.filter((value, field) => {
            return typeof (value) == "boolean" ? !value : value.trim() === '';
        });
        const hasFilledAllValues = unfilledValues.count() === 0;

        const hasSamePassword = fields.get('password') === fields.get('repeat_password');
        const hasValidEmail = simpleEmailRegexp.test(fields.get('email'));
        if (hasSamePassword && hasFilledAllValues && hasValidEmail) {
            this.props.registerUser(this.state.fields.toJS(), this.props.routingLocation.query.userroletype ? this.props.routingLocation.query.userroletype : '');
        } else {
            let errors = fromJS({});

            unfilledValues.map((value, field) => {
                errors = errors.set(field, true);
            });
            if (!hasFilledAllValues) {
                if (errors.get('iagree') == true) {
                    toast.error("You must agree to our Privacy statement", {
                        position: toast.POSITION.TOP_CENTER
                    });
                    errors = errors.set('iagree', true);
                } else
                    toast.error("Niet alle vereiste velden zijn ingevuld.", {
                        position: toast.POSITION.TOP_CENTER
                    });
            }
            else if (!hasValidEmail) {
                toast.error("The email address is not valid", {
                    position: toast.POSITION.TOP_CENTER
                });
                errors = errors.set('email', true);
            }

            this.setState({
                errors: this.state.errors.merge(errors),
            });
        }
    }

    render() {
        const editMode = this.props.userToEdit != null;
        const {
            fields,
            errors,
        } = this.state;

        return (
            <div className="container">
                {!editMode && <h2> Register a User </h2>}
                {editMode && <h2> Edit the User </h2>}
                <hr className="style14" />
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
                    <label className="col-sm-2 col-form-label control-label" htmlFor="lastname_field">Lastname</label>
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
                    <label className="col-sm-2 col-form-label control-label" htmlFor="zipcode_field">Zipcode</label>
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
                    <label className="col-sm-2 col-form-label control-label" htmlFor="phonenumber_field">Phone Number</label>
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
                    <label className="col-sm-2 col-form-label" htmlFor="phonenumbermobile_field">Phone Number Mobile</label>
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
                        <label>You agree with the
                            <Link to="/users/privacy-statement" target="_blank"> Privacy Statement</Link>
                        </label>
                    </div>
                </div>
                <div className="form-group row required" >
                    <label className="col-sm-2 col-form-label"></label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <div className="custom-control custom-checkbox">
                            <input type="checkbox" className="custom-control-input" id='iagree' name={'iagree'} field={'iagree'} value={fields.get('iagree')} onChange={(value) => this.handleCheckboxChange(value)} hasError={errors.has('iagree')} />
                            <label className="custom-control-label" htmlFor="iagree">I agree to the Privacy statement<span className="checkmark"></span></label>
                        </div>
                    </div>
                </div>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label"></label>
                    <div className="col-md-2" />
                    <div className="col-md-6">
                        <div className="custom-control custom-checkbox">
                            <input type="checkbox" className="custom-control-input" id="newsletter" />
                            <label className="custom-control-label" htmlFor="newsletter">Sign me up for the newsletter<span className="checkmark"></span></label>
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
                                className="btn btn-primary"
                                type="button"
                                value="Confirm"
                                onClick={this.handleSubmit}
                            />
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default connect(
    (state) => {
        return {
            userToEdit: userToEditSelector(state),
            routingLocation: routingLocationSelector(state),
        }
    },
    dispatch => bindActionCreators({
        registerUser: registerUserAction,
        fetchUserWithId: fetchUserWithIdAction,
        clearUserWithId: clearUserWithIdAction,
    }, dispatch),
)(Register);
