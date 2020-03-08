import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import configurePasswordAction from '../../store/users/actions/configurePassword';
import ReactPasswordStrength from 'react-password-strength';
import { toast } from 'react-toastify';
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';

class ConfigurePassword extends Component {

    constructor(props) {
        super(props);
        this.state = {
            passwordTextBox: {},
            confirmPasswordTextBox: {}
        };

        this.handleChangePassword = this.handleChange.bind(this, 'passwordTextBox');
        this.handleChangeConfirm = this.handleChange.bind(this, 'confirmPasswordTextBox');
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(k, v) {
        this.setState({
            [k]: v
        });
    }

    handleSubmit() {
        const hasSamePassword = this.state.passwordTextBox.password === this.state.confirmPasswordTextBox.password;
        const hasValidValues = (this.state.passwordTextBox.isValid && this.state.confirmPasswordTextBox.isValid);        
        if (hasSamePassword && hasValidValues) {
            this.props.configurePassword(this.state.confirmPasswordTextBox.password);
        } else {
            if (!hasSamePassword)
                toast.error("Passwords do not match", {
                    position: toast.POSITION.TOP_CENTER
                });
            else if (!hasValidValues)
                toast.error("The password does not meet the requirements. Passwords must be at least 8 characters and one capital letter", {
                    position: toast.POSITION.TOP_CENTER
                });
        }
    }

    render() {
        const {
            inputProps = {
                placeholder: "Choose your password...",
                autoFocus: true,
                className: 'another-input-prop-class-name'
            },
            fields,
        } = this.state;

        return (
            <span>
                <h3> Configure Password </h3>
                <hr className="style14" />
                <div className="form-group">
                    <label htmlFor="password_field">Password:</label>
                    <ReactPasswordStrength
                        minLength={8}
                        maxLenght={30}
                        minScore={2}
                        inputProps={{ ...inputProps, id: 'passwordField' }}
                        changeCallback={(value) => this.handleChangePassword(value)}
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="confirmPassword_field">Confirm Password:</label>
                    <ReactPasswordStrength
                        minLength={8}
                        maxLenght={30}
                        minScore={2}
                        inputProps={{ placeholder: 'Repeat your password...', id: 'passwordConfirmField' }}
                        changeCallback={(value) => this.handleChangeConfirm(value)}
                    />
                </div>
                <hr className="style12" />
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
                <hr className="style12" />
                <h3>Password Requirements: </h3>
                <hr className="style14" />
                <li>Minimum 8 characters</li>
                <li>Maximum 30 characters</li>
                <li>Minimum 1 special character</li>
                <li>Minimum 1 number</li>
                <li>Minimum 1 capital letter</li>
            </span>
        );
    }
}

export default connect(
    (state) => {
        return {
            routingLocation: routingLocationSelector(state)
        }
    },
    dispatch => bindActionCreators({
        configurePassword: configurePasswordAction,
    }, dispatch),
)(ConfigurePassword);
