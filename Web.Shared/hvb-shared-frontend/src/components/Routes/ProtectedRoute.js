import React, { Component } from 'react';
import { Route } from 'react-router';
import { Redirect } from 'react-router-dom';
import { userLoggedInSelector } from '../../store/selectors/authSelectors';
import { connect } from 'react-redux';

class ProtectedRoute extends Component {
    render() {
        const { component: Component, ...props } = this.props;
        return (
            <Route
                {...props}
                render={(props) => {
                    return (
                        this.props.userLoggedIn ?
                            <Component {...props} /> :
                            <Redirect to='/users/login' />
                    );      
                }}
            />
        )
    }
}

export default connect(
    (state) => {
        return {
            userLoggedIn: userLoggedInSelector(state),
        }
    },
    null,
)(ProtectedRoute);