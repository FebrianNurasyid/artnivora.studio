import { fromJS } from 'immutable';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import loginUserAction from 'hvb-shared-frontend/src/store/actions/loginUser';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import ForgotPassword from '../Modals/ForgotPassword';
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';
import './Styles.css';
import { Redirect } from 'react-router-dom'
import banner from './banner.png';

class Login extends Component {

    constructor(props) {
        super(props);
        this.state = {
            fields: fromJS({
                username: '',
                password: '',
            }),
            errors: fromJS({})
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
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

        const unfilledValues = fields.filter((value) => {
            return value.trim() === '';
        });
        const hasFilledAllValues = unfilledValues.count() === 0;

        if (hasFilledAllValues) {
            this.props.loginUser(this.state.fields.toJS());
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

    render() {
        const {
            fields,
            errors,
        } = this.state;

        const { loggedInUser } = this.props;

        if (loggedInUser !== undefined) return <Redirect to='/overview' />;
        return (
            <div className="flex-container">
                    <div className="form-group" >
                        <div className="imgesc">
                        <img src={banner}  alt="HTML5" ></img>
                        </div>
                    </div>

                    <div className="form-group padarea">
                        <h2> Please Login . . .  </h2>
                        <hr className="style14" />
                        <div className="form-group row col-md-6">
                            <label htmlFor="username_field">Username</label>
                            <TextInputField
                                field={'username'}
                                value={fields.get('username')}
                                onChange={(field, value) => this.handleChange(field, value)}
                                hasError={errors.has('username')}
                            />
                        </div>
                        <div className="form-group row col-md-6">
                            <label htmlFor="password_field">Password</label>
                            <TextInputField
                                field={'password'}
                                value={fields.get('password')}
                                onChange={(field, value) => this.handleChange(field, value)}
                                hasError={errors.has('password')}
                                password
                            />
                        </div>
                        {errors.valueSeq().map((error) => {
                            if (typeof (error) === "string") {
                                return (
                                    <div key={`error-${error}`} className={"formError"}>
                                        {error}
                                    </div>
                                );
                            }
                        })}
                        <HVBButton
                            className="btn btn-primary"
                            type="button"
                            value="LOGGEDIN"
                            onClick={this.handleSubmit}
                        >Login</HVBButton>
                        <hr />
                    {/*<ForgotPassword initialModalState={false} />*/}
                    </div>
                </div>
        );
    }
}

export default connect(
    (state) => {
        return {
            routingLocation: routingLocationSelector(state),
            loggedInUser: state.auth.loggedInUser
        }
    },
    dispatch => bindActionCreators({
        loginUser: loginUserAction
    }, dispatch),
)(Login);
