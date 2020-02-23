import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import configurePasswordAction from '../../store/users/actions/configurePassword';
import ReactPasswordStrength from 'react-password-strength';
import { toast } from 'react-toastify';


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
        const isRecoveryPassword = this.props.routingLocation.query.recoverytoken ? true : false;
        const activateToken = isRecoveryPassword ? this.props.routingLocation.query.recoverytoken : this.props.routingLocation.query.activatetoken;
        if (hasSamePassword && hasValidValues) {
            this.props.configurePassword(this.state.confirmPasswordTextBox.password, activateToken ? activateToken : '', isRecoveryPassword);
        } else {
            if (!hasSamePassword)
                toast.error("Wachtwoorden komen niet overeen", {
                    position: toast.POSITION.TOP_CENTER
                });
            else if (!hasValidValues)
                toast.error("Het wachtwoord voldoet niet aan de eisen. Wachtwoorden moeten minimaal 8 karakters zijn en ��n hoofdletter", {
                    position: toast.POSITION.TOP_CENTER
                });
        }
    }

    render() {
        const {
            inputProps = {
                placeholder: "Kies uw wachtwoord...",
                autoFocus: true,
                className: 'another-input-prop-class-name'
            },
            fields,
        } = this.state;

        return (
            <span>
                <h1> Configureer wachtwoord </h1>
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
                        inputProps={{ placeholder: 'Herhaal uw wachtwoord..', id: 'passwordConfirmField' }}
                        changeCallback={(value) => this.handleChangeConfirm(value)}
                    />
                </div>
                <input
                    className="btn btn-primary"
                    type="button"
                    value="Bevestig"
                    onClick={this.handleSubmit}
                />
                <div>
                    <h3>Wachtwoord eisen: </h3>
                    <ul>
                        <li>Minimaal 8 characters</li>
                        <li>Maximaal 30 characters</li>
                        <li>Minimaal 1 speciaal teken</li>
                        <li>Minimaal 1 nummer</li>
                        <li>Minimaal 1 hoofdletter</li>
                    </ul>
                </div>
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
