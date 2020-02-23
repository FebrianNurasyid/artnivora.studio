import React, { Component } from 'react';

class TextInputField extends Component {
    render() {

        let inputType = 'text';

        if (this.props.password) {
            inputType = 'password';
        } else if (this.props.email) {
            inputType = 'email';
        }

        const extraClass = this.props.hasError ? 'is-invalid' : '';
        return (
            <input
                className={`form-control ${extraClass}`}
                id={`${this.props.field}_field`}
                disabled={this.props.disabled}
                type={inputType}
                value={this.props.value}
                placeholder={this.props.placeholder}
                onChange={(event) => {
                    this.props.onChange(this.props.field, event.target.value);
                }}
            />
        );
    }
}

export default (TextInputField);
