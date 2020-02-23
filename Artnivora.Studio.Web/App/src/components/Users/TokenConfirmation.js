import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import fetchTokenConfirmationAction from '../../store/users/actions/fetchTokenConfirmation';
import {
    activationStateSelector,
    validTokenStateSelector,
} from '../../store/users/selectors/userSelectors';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import ForgotPassword from './Modals/ForgotPassword';

class TokenConfirmation extends Component {

    constructor(props) {
        super(props);
        this.state = {
            isRecoveryPassword: false
        };
    }

    componentDidMount() {
        const {
            routingLocation,
        } = this.props;
        if (routingLocation && routingLocation.query) {
            this.setState.isRecoveryPassword = this.props.routingLocation.query.recoverytoken ? true : false;
            const token = this.setState.isRecoveryPassword ? routingLocation.query['recoverytoken'] : routingLocation.query['activatetoken'];
            if (token) {
                this.props.fetchTokenConfirmation(token, this.setState.isRecoveryPassword);
            }
        }
    }

    render() {
        const {
            activationState,
            validTokenState,
        } = this.props;        
        if (activationState === false) {
            return (
                <span>
                    {renderActivationFailed(this.props)}
                </span>
            );
        }
        else if (activationState === true && validTokenState === true && !this.setState.isRecoveryPassword) {
            return (
                <span>
                    {renderActivationSuccess(this.props)}
                </span>
            );
        } else if (activationState === true && validTokenState === true && this.setState.isRecoveryPassword) {
            return (
                <span>
                    {renderValidToken(this.props)}
                </span>
            );
        }
        else if (activationState === true && validTokenState === false) {
            return (
                <span>
                    {renderForgotPassword(this.props)}
                </span>
            );
        } else {
            return (
                <span>
                    {renderActivationInProgress(this.props)}
                </span>
            );
        }
    }
}

function renderActivationFailed(props) {
    return (
        <div className="container">
            <p>&nbsp;</p>
            <h2>Er is iets mis gegaan tijdens uw activatie.</h2>
            <p>Something went wrong while activating your account.</p>
            <hr className="style14" />
            <p>&nbsp;</p>
        </div>
    );
}

function renderActivationInProgress(props) {
    return (
        <div className="container">
            <p>&nbsp;</p>
            <h2>Uw account wordt geactiveerd.. een moment geduld.</h2>
            <p>Your account is being activated.. a moment please.</p>
            <hr className="style14" />
            <p>&nbsp;</p>
        </div>
    );
}

function renderActivationSuccess(props) {
    return (
        <div className="container">
            <p>&nbsp;</p>
            <h2>Het activeren is gelukt, U wordt doorverwezen naar de inlog pagina</h2>
            <p>Your account has been activated, you will be redirected to the login page.</p>
            <hr className="style14" />
            <p>&nbsp;</p>
        </div>
    );
}

function renderForgotPassword(props) {
    return (
        <div className="container">
            <h2>Uw toegangstoken is niet geldig of is verlopen.. Stel uw wachtwoord opnieuw in.</h2>
            <p>Your access token is not valid or has expired.. Please reset your password.</p>
            <hr className="style14" />
            <ForgotPassword initialModalState={false} />
        </div>
    );
}

function renderValidToken(props) {
    return (
        <div className="container">
            <p>&nbsp;</p>
            <h2>Uw hersteltoken is gevalideerd, u wordt omgeleid om uw nieuwe wachtwoord te configureren.</h2>
            <p>Your recovery token is validated, you will be redirected to configure your new password.</p>
            <hr className="style14" />
            <p>&nbsp;</p>
        </div>
    );
}

export default connect(
    (state) => {
        return {
            routingLocation: routingLocationSelector(state),
            activationState: activationStateSelector(state),
            validTokenState: validTokenStateSelector(state)
        }
    },
    dispatch => bindActionCreators({
        fetchTokenConfirmation: fetchTokenConfirmationAction,
    }, dispatch)
)(TokenConfirmation);