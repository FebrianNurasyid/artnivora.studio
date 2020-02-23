import { fromJS } from 'immutable';
import { bindActionCreators } from 'redux';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import blankImage from '../images/blankimg.png';
import {
    VOLUNTEER,
    PARTICIPANT,
    GUEST
} from 'hvb-shared-frontend/src/constants/roles';

import ParticipantProfile from './ParticipantProfile';
import VolunteerProfile from './VolunteerProfile';

class ProfilePage extends Component {

    render() {
        const userRoles = this.props.loggedInUser == undefined ? "" : this.props.loggedInUser.roles[0];
        return (
            <div>
                {userRoles === VOLUNTEER && <VolunteerProfile />}
                {userRoles === PARTICIPANT && <ParticipantProfile />}
            </div>);
    }
}

export default connect(
    (state) => {
        return { loggedInUser: state.auth.loggedInUser }
    },
    dispatch => { }
)(ProfilePage);