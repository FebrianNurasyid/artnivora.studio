import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { MdAttachFile } from "react-icons/md";
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';
import './MessageReceived.css';
import ItemMessages from './ItemMessages';
import 'font-awesome/css/font-awesome.min.css';
import fetchMessagesAction from '../../store/messages/actions/fetchMessages';
import { Map } from 'immutable';
import { filteredMessagesSelector, linkInfoSelector } from '../../store/messages/selectors/messagesSelectors';
import { FaCameraRetro } from 'react-icons/fa';

class MessageReceived extends Component {
    constructor(props) {
        super(props)
        this.state = {
            itemMessages: Map(),
            linkInfo: [],
            currentOffset: 0,
            currentLimit: 0,
        }
        this.handleAllChecked = this.handleAllChecked.bind(this);
        this.handleCheckChildElement = this.handleCheckChildElement.bind(this);
        this.handleRefresh = this.handleRefresh.bind(this);
        this.handleNext = this.handleNext.bind(this);
        this.handlePrevious = this.handlePrevious.bind(this);
        this.handleClickRow = this.handleClickRow.bind(this);
    }

    componentDidMount() {
        this.props.fetchMessages();
        this.setState({ intervalId: setInterval(this.props.fetchMessages(null, null, this.state.linkInfo ? this.state.linkInfo.url : null), 30000) });
    }

    componentWillUnmount() {
        if (this.state.intervalId) {
            clearInterval(this.state.intervalId);
        }
    }

    static getDerivedStateFromProps = (props, state) => {

        const getValueFromLinkInfo = (linkInfo, isLimit) => {
            if (linkInfo) {
                if (!isLimit) {
                    return parseInt(linkInfo.url.substring(linkInfo.url.indexOf("=") + 1, linkInfo.url.indexOf("&")));
                }
                else {
                    return parseInt(linkInfo.url.substring(linkInfo.url.lastIndexOf("=") + 1, linkInfo.url.length))
                }
            }
            else return 0;
        }

        if (state && state.itemMessages.size == 0) {
            if (props && props.linkInfo) {
                return {
                    itemMessages: props.messages.merge(state.itemMessages),
                    linkInfo: props.linkInfo,
                    currentOffset: getValueFromLinkInfo(props.linkInfo, false),
                    currentLimit: getValueFromLinkInfo(props.linkInfo, true),
                }
            } else
                return {
                    itemMessages: props.messages.merge(state.itemMessages),
                    linkInfo: props.linkInfo,
                    currentOffset: 0,
                    currentLimit: 0
                };
        }
        else {
            if (props && props.linkInfo) {
                return {
                    itemMessages: state.itemMessages,
                    linkInfo: props.linkInfo,
                    currentOffset: getValueFromLinkInfo(props.linkInfo, false),
                    currentLimit: getValueFromLinkInfo(props.linkInfo, true),
                }
            }
        }
        // Return null if the state hasn't changed
        return null;
    }

    handleAllChecked = (event) => {
        const itemMessages = this.state.itemMessages
            .map(message => message.set('isChecked', event.target.checked));
        this.setState({ itemMessages: itemMessages })
    }

    handleCheckChildElement = (event) => {
        const itemMessages = this.state.itemMessages
            .map(message => {
                if (message.get('id') === event.target.value)
                    return message.set('isChecked', event.target.checked);
                return message;
            })

        this.setState({ itemMessages: itemMessages })
    }

    handleClickRow = (value) => {
        return this.props.history.push(`/messages/detailmessage?id=${value}`);
    }

    handleRefresh() {
        this.setState({ itemMessages: Map() });
        this.props.fetchMessages(null, null, this.state.linkInfo.url);
    }
    handlePrevious() {
        this.setState({ itemMessages: Map() });
        this.props.fetchMessages(null, null, this.state.linkInfo.prevUrl);
    }

    handleNext() {
        this.setState({ itemMessages: Map() });
        this.props.fetchMessages(null, null, this.state.linkInfo.nextUrl);
    }
    render() {
        return (
            <div className="messageReceivedContainer">
                <div className="messageReceivedHeader">
                    <h2>Mijn ontvangen berichten</h2>
                    <HVBButton
                        className="btn btn-primary"
                        type="button"
                        value="Inloggen"
                    >Nieuw</HVBButton>
                </div>
                <div className="messageReceivedHeader">
                    <div className="btn-group">
                        <HVBButton
                            type="button"
                            className="btn btn-default waves-effect waves-light"
                            onClick={this.handleRefresh}
                        >
                            <i className="fa fa-refresh" aria-hidden="true"></i>
                        </HVBButton>
                    </div>
                    <div className="btn-group pull-right">
                        <button onClick={this.handlePrevious} type="button" className="btn btn-default waves-effect"><i className="fa fa-chevron-left" aria-hidden="true"></i></button>
                        <button onClick={this.handleNext} type="button" className="btn btn-default waves-effect"><i className="fa fa-chevron-right" aria-hidden="true"></i></button>
                    </div>
                </div>

                <hr className="headerLine" />
                <div className="row">
                    <div className="cont">
                        <div className="custom-control custom-checkbox">
                            <input type="checkbox" onClick={this.handleAllChecked} key="0" id="headCheck" />
                            <label className="checkmark" htmlFor="headCheck"></label>
                        </div>
                    </div>
                    <div className="col">Van</div>
                    <div className="col-6">Onderwerp</div>
                    <div className="col">
                        <MdAttachFile size={16} />
                    </div>
                    <div className="col">Datum / tijd</div>
                </div>
                {
                    this.state.itemMessages.toList().map((message) => {
                        return (<ItemMessages handleCheckChildElement={this.handleCheckChildElement} handleClickRow={this.handleClickRow}  {...message.toJS()} />)
                    })
                }

                <div>
                    Showing {this.state.currentOffset == 0 ? this.state.currentLimit : this.state.currentOffset + this.state.currentLimit} of {this.state.linkInfo ? (this.state.linkInfo.totalCount) : 0}
                </div>
            </div>
        );
    }
}

export default connect(
    (state) => {
        return {
            messages: filteredMessagesSelector(state),
            linkInfo: linkInfoSelector(state)
        }
    },
    dispatch => bindActionCreators({
        fetchMessages: fetchMessagesAction,
    }, dispatch),
)(MessageReceived);