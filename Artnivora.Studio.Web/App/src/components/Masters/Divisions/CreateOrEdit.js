import { fromJS } from 'immutable';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import clearUserWithIdAction from '../../../store/users/actions/clearUserWithId';
import fetchUserWithIdAction from '../../../store/users/actions/fetchUserWithId';
import registerUserAction from '../../../store/users/actions/registerUser';
import { userToEditSelector } from '../../../store/users/selectors/userSelectors';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import styles from '../Styles.css';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';
import { goBack } from 'react-router-redux';

class CreateOrEdit extends Component {

    constructor(props) {
        super(props);
        this.state = {
            fields: fromJS({
                divisionname: '',
            }),
            errors: fromJS({})
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        const {
            routingLocation
        } = this.props;
        this.props.clearUserWithId();
        if (routingLocation && routingLocation.query) {
            const userId = routingLocation.query['id'];
            if (userId) {
                this.props.fetchUserWithId(userId);
            }
        }
    }

    componentDidUpdate(prevProps) {
        const { userToEdit } = this.props;
        if (userToEdit && !prevProps.userToEdit) {
            this.setState({
                fields: fromJS({
                    divisionname: '',
                }),
                errors: fromJS({}),
            })
        }
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
            this.props.registerUser(this.state.fields.toJS());
            alert("Save Here");
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
        const editMode = this.props.userToEdit != null;
        const {
            fields,
            errors,
        } = this.state;

        return (
            <span>
                {!editMode && <h3> New Division </h3>}
                {editMode && <h2> Edit the Division </h2>}
                <hr className="style12" />
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label control-label" htmlFor="divisionname_field">Division Name</label>
                    <div className="col-md-4">
                        <TextInputField
                            field={'divisionname'}
                            value={fields.get('divisionname')}
                            onChange={(field, value) => this.handleChange(field, value)}
                            hasError={errors.has('divisionname')}
                        />
                    </div>
                </div>
                {
                    errors.valueSeq().map((error) => {
                        if (typeof (error) === "string") {
                            return (
                                <div key={`error-${error}`} className={styles.error}>
                                    {error}
                                </div>
                            );
                        }
                    })
                }
                <div className="form-group">
                    <label className="col-sm-2 col-form-label control-label"></label>
                    <HVBButton
                        className="btn btn-primary"
                        type="button"
                        onClick={this.handleSubmit}
                    >Save</HVBButton>
                    <HVBButton
                        className="btn btn-primary"
                        extraClassName="btn-cancel"
                        type="button"
                        onClick={this.props.history.goBack}
                    >Cancel</HVBButton>
                </div>
            </span>
        );
    }
}

export default connect(
    (state) => {
        return {
            userToEdit: userToEditSelector(state),
            routingLocation: routingLocationSelector(state),
        }
    },
    dispatch => bindActionCreators({
        registerUser: registerUserAction,
        fetchUserWithId: fetchUserWithIdAction,
        clearUserWithId: clearUserWithIdAction,
    }, dispatch),
)(CreateOrEdit);
