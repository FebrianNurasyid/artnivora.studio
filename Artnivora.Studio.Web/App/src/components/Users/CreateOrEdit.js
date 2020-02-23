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
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';

class CreateOrEdit extends Component {

    constructor(props) {
        super(props);
        this.state = {
            fields: fromJS({
                username: '',
                email: '',
                password: '',
                repeat_password: '',
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

    handleSubmit() {
        const { fields } = this.state;

        this.setState({
            errors: fromJS({}),
        });

        const simpleEmailRegexp = new RegExp('[^@]+@[^\.]+\..+');

        const unfilledValues = fields.filter((value) => {
            return value.trim() === '';
        });
        const hasFilledAllValues = unfilledValues.count() === 0;

        const hasSamePassword = fields.get('password') === fields.get('repeat_password');
        const hasValidEmail = simpleEmailRegexp.test(fields.get('email'));
        console.log(unfilledValues);
        if (hasSamePassword && hasFilledAllValues && hasValidEmail) {
            this.props.registerUser(this.state.fields.toJS());
        } else {
            let errors = fromJS({});

            unfilledValues.map((value, field) => {
                errors = errors.set(field, true);
            });
            if (!hasValidEmail) {
                errors = errors.set('email', 'The email address is not valid');
            }

            if (!hasSamePassword) {
                errors = errors.set('password', 'The passwords are not the same');
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
                {!editMode && <h3> Create new user </h3>}
                {editMode && <h2> Edit the User </h2>}
                <hr className="style12" />
                <div className="form-group">
                    <label htmlFor="username_field">Username:</label>
                    <TextInputField
                        field={'username'}
                        value={fields.get('username')}
                        onChange={(field, value) => this.handleChange(field, value)}
                        hasError={errors.has('username')}
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="email_field">Email:</label>
                    <TextInputField
                        field={'email'}
                        value={fields.get('email')}
                        onChange={(field, value) => this.handleChange(field, value)}
                        hasError={errors.has('email')}
                        email
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="password_field">Password:</label>
                    <TextInputField
                        field={'password'}
                        value={fields.get('password')}
                        onChange={(field, value) => this.handleChange(field, value)}
                        hasError={errors.has('password')}
                        password
                    />

                    <label htmlFor="repeat_password_field">Repeat Password:</label>
                    <TextInputField
                        field={'repeat_password'}
                        value={fields.get('repeat_password')}
                        onChange={(field, value) => this.handleChange(field, value)}
                        hasError={errors.has('password')}
                        password
                    />
                    {errors.valueSeq().map((error) => {
                        if (typeof (error) === "string") {
                            return (
                                <div key={`error-${error}`} className={styles.error}>
                                    {error}
                                </div>
                            );
                        }
                    })}
                </div>                

                <div className="form-group">
                    <HVBButton
                        className="btn btn-primary"
                        type="button"                        
                        onClick={this.handleSubmit}
                    >Submit</HVBButton>
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
        }
    },
    dispatch => bindActionCreators({
        registerUser: registerUserAction,
        fetchUserWithId: fetchUserWithIdAction,
        clearUserWithId: clearUserWithIdAction,
    }, dispatch),
)(CreateOrEdit);
