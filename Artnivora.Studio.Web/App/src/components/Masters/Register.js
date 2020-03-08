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
import { toast } from 'react-toastify';
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';

class Register extends Component {

    constructor(props) {
        super(props);
        this.state = {
            fields: fromJS({
                username: '',
                firstname: '',
                lastname: '',
                phonenumber: '',
                email: '',
                password: 'secret',
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
            return (f != 'password')
        })
        const unfilledValues = nonMandatoryFields.filter((value, field) => {
            return typeof (value) == "boolean" ? !value : value.trim() === '';
        });
        const hasFilledAllValues = unfilledValues.count() === 0;
        
        const hasValidEmail = simpleEmailRegexp.test(fields.get('email'));
        if (hasFilledAllValues && hasValidEmail) {
            this.props.registerUser(this.state.fields.toJS(), 'Participant');
            this.props.history.goBack();
        } else {
            let errors = fromJS({});

            unfilledValues.map((value, field) => {
                errors = errors.set(field, true);
            });
            if (!hasFilledAllValues) {
                toast.error("Not all required fields are filled.", {
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
            <span>
                {!editMode && <h3> New User </h3>}
                {editMode && <h3> Edit the User </h3>}
                <hr className="style14" />
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="title_field">Username</label>
                    <div className="col-md-4">
                        <TextInputField
                            field={'username'}
                            value={fields.get('username')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('username')}
                        />
                    </div>
                    <label className="col-sm-2 col-form-label control-label" htmlFor="category_field">Email</label>
                    <div className="col-md-4">
                        <TextInputField
                            field={'email'}
                            value={fields.get('email')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('email')}
                        />
                    </div>
                </div>

                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="title_field">Fistname</label>
                    <div className="col-md-4">
                        <TextInputField
                            field={'firstname'}
                            value={fields.get('firstname')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('firstname')}
                        />
                    </div>
                    <label className="col-sm-2 col-form-label control-label" htmlFor="category_field">Lastname</label>
                    <div className="col-md-4">
                        <TextInputField
                            field={'lastname'}
                            value={fields.get('lastname')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('lastname')}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="title_field">Phone Number</label>
                    <div className="col-md-4">
                        <TextInputField
                            field={'phonenumber'}
                            value={fields.get('phonenumber')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('phonenumber')}
                        />
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
