import React, { Component } from 'react';

class ThankYou extends Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <span>
                <div className="container">
                    <p>&nbsp;</p>
                        <h2>Registration successful</h2>
                        <p>Thank you for your registration. We have sent an activation email with which you can configure a password and get access to my holiday vacation.</p>
                    <p>&nbsp;</p>
                </div>
            </span>
        );
    }
}

export default (ThankYou);