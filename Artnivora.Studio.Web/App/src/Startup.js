import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import loginUserWithBearerTokenAction from 'hvb-shared-frontend/src/store/actions/loginUserWithBearerToken';

class Startup extends Component {
    componentWillMount() {
        this.props.loginUserWithBearerToken();
    }

    render() {
        return (
            <span />
        );
    }
}

export default connect(
    null,
    dispatch => bindActionCreators({
        loginUserWithBearerToken: loginUserWithBearerTokenAction,
    }, dispatch),
)(Startup);