import { fromJS } from 'immutable';
import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import { Button, Modal, ModalFooter, ModalHeader, ModalBody } from 'reactstrap';
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import resetPasswordAction from '../../../store/users/actions/resetPassword';

class ForgotPassword extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            modal: props.initialModalState,
            fade: true,
            fields: fromJS({
                email: '',
            }),
            errors: fromJS({})
        };

        this.toggle = this.toggle.bind(this);
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
        let errors = fromJS({});
        this.setState({
            errors: fromJS({}),
        });

        const simpleEmailRegexp = new RegExp('[^@]+@[^\.]+\..+');
        const unfilledValues = fields.filter((value, field) => {
            return typeof (value) == "boolean" ? !value : value.trim() === '';
        });
        const hasFilledAllValues = unfilledValues.count() === 0;
        const hasValidEmail = simpleEmailRegexp.test(fields.get('email'));

        if (hasFilledAllValues && hasValidEmail) {
            this.props.resetPassowrd(fields.get('email'));
            this.toggle();
        }
        else {
            if (!hasFilledAllValues) {                
                errors = errors.set('email', true);
            }
            else if (!hasValidEmail) {
                toast.error("het e-mailadres is niet geldig.", {
                    position: toast.POSITION.TOP_CENTER
                });
                errors = errors.set('email', true);
            }

            this.setState({
                errors: this.state.errors.merge(errors),
            });
        }
    }

    toggle() {
        this.setState({
            modal: !this.state.modal,
            fade: !this.state.fade

        });
    }

    render() {
        const {
            fields,
            errors,
        } = this.state;

        return (
            <div>
                <label><Link to='#' onClick={this.toggle}>Forgot Password?</Link></label>
                <Modal isOpen={this.state.modal} toggle={this.toggle}
                    fade={this.state.fade}
                    className={this.props.className}>
                    <ModalHeader toggle={this.toggle}>Forgot Password</ModalHeader>
                    <ModalBody>
                        <div className="form-group row required">
                            <label className="col-sm-2 col-form-label control-label" htmlFor="email_field">Email</label>
                            <div className="col-md-2" />
                            <div className="col-md-6">
                                <TextInputField
                                    field={'email'}
                                    value={fields.get('email')}
                                    onChange={(field, value) => this.handleChange(field, value)}
                                    hasError={errors.has('email')}
                                />
                            </div>
                        </div>
                    </ModalBody>
                    <ModalFooter>
                        <Button color="primary" onClick={this.handleSubmit}>Confirmation</Button>{' '}
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}

export default connect(  
    (state) => {
        return {      
            routingLocation: routingLocationSelector(state),
        }
    },
    dispatch => bindActionCreators({
        resetPassowrd: resetPasswordAction,        
    }, dispatch),
)(ForgotPassword);