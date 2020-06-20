import React, { Component } from 'react';
import "./overview.css";
import imagelogin from '../images/login.jpg';
import fetchUserLoginDashboard from '../../store/dashboard/action/fetchUserLoginDashboard';
import { FaSignOutAlt, FaSuitcase, FaRegEnvelope, FaPencilAlt, FaUpload, FaFont } from "react-icons/fa";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { userDashboardSelector } from '../../store/users/selectors/userSelectors';
import { Link } from 'react-router-dom';

class Overview extends Component {

    constructor(props) {
        super(props);
        this.state = {}
    }

    componentDidMount() {
        this.props.fetchDashboard();
    }

    render() {
        const hideNotification = this.props.userLoginDashboard
            && this.props.userLoginDashboard.userProfileIsComplete;

        const notificationClassName = hideNotification ? 'hideNotification' : 'showNotification';
        return (
            <div className="overview-container">
                <div className="overview-container-left">
                    <div className="">
                        <h3>Hi <b>{this.props.userLoginDashboard.firstName}</b>, welcome back.</h3>
                        <hr className="style14" />
                        <div className={`yellowsection ${notificationClassName}`}>                            
                            <h5><i>Keep remain, Teamwork Make A Dream Works. Believe It . . .</i></h5>
                            <hr className="style14" />
                                Stay creative, innovative and positive thinking. . .                                
                            {/*<Link to="/users/userprofile">Setup profile</Link>*/}
                        </div>                                                
                        <div className="smallFlexBox">
                            <div className="holidaybox text-center">
                                <span className="holidaybox-information">
                                    <span className="holidaybox-information-header">
                                        <FaPencilAlt className="holidayboxFa" />
                                    </span>
                                    <span>
                                        Production
                                    </span>
                                </span>
                                <span className="holidaybox-information">
                                    <span className="holidaybox-information-header">
                                        <div>{this.props.userLoginDashboard.registeredCount ?? 0}</div>
                                    </span>
                                    <span>
                                        Pending Item
                                    </span>
                                </span>
                            </div>

                            <div className="holidaybox text-center">
                                <span className="holidaybox-information">
                                    <span className="holidaybox-information-header">
                                        <FaSuitcase className="holidayboxFa" />
                                    </span>
                                    Packaging
                                </span>
                                <span className="holidaybox-information">
                                    <span className="holidaybox-information-header">
                                        <div>{this.props.userLoginDashboard.registeredCount ?? 0}</div>
                                    </span>
                                    <span>
                                        Pending Item
                                    </span>
                                </span>
                            </div>
                        </div>
                        <div className="smallFlexBox">
                            <div className="holidaybox text-center">
                                <span className="holidaybox-information">
                                    <span className="holidaybox-information-header">
                                        <FaFont className="holidayboxFa" />
                                    </span>
                                    <span>
                                        Keywording
                                    </span>
                                </span>
                                <span className="holidaybox-information">
                                    <span className="holidaybox-information-header">
                                        <div>{this.props.userLoginDashboard.unReadCount ?? 0}</div>
                                    </span>
                                    <span>
                                        Pending Item
                                    </span>
                                </span>
                            </div>

                            <div className="holidaybox text-center">
                                <span className="holidaybox-information">
                                    <span className="holidaybox-information-header">
                                        <FaUpload className="holidayboxFa" />
                                    </span>
                                    <span>
                                        Uploading
                                    </span>
                                </span>
                                <span className="holidaybox-information">
                                    <span className="holidaybox-information-header">
                                        <div>{this.props.userLoginDashboard.requestCount ?? 0}</div>
                                    </span>
                                    <span>
                                        Pending Item
                                    </span>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        );
    }
}

function mapStateToProp(state, ownprop) {
    return {
        userLoginDashboard: userDashboardSelector(state),
    }
}

export default connect(
    mapStateToProp,
    dispatch => bindActionCreators({
        fetchDashboard: fetchUserLoginDashboard,
    }, dispatch)
)(Overview);