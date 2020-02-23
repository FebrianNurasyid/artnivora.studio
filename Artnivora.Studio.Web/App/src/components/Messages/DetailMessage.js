import { fromJS } from 'immutable';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';
import './MessageReceived.css';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import clearMessageWithIdAction from '../../store/messages/actions/clearMessageWithId';
import setMessageAsReadAction from '../../store/messages/actions/SetMessageAsRead';
import fetchMessagesWithIdAction from '../../store/messages/actions/fetchMessageWithId';
import { messageReadSelector } from '../../store/messages/selectors/messagesSelectors';

class DetailMessage extends Component {
    constructor(props) {
        super(props)
        this.state = {
            fields: fromJS({
                body: '',
                subject: '',
                messageSendDateTime: '',
                id: '',
            }),
            mailboxId: ''
        };
    }

    componentDidMount() {
        const {
            routingLocation
        } = this.props;
        
        this.props.clearMessageWithId();
        if (routingLocation && routingLocation.query) {
            const mailboxId = routingLocation.query['id'];
            if (mailboxId) {
                this.props.fetchMessageWithId(mailboxId);
            }
        }

    }

    componentDidUpdate(prevProps) {
        const { message } = this.props;
        if (message && !prevProps.message) {

            this.setState({
                fields: fromJS({
                    body: message.body.replace(/</g, "&lt;").replace(/>/g, "&gt;"),
                    subject: message.subject,
                    messageSendDateTime: message.messageSendDateTime,
                    id: message.id,
                }),
            })
            const {
                routingLocation
            } = this.props;
            this.props.clearMessageWithId();
            if (routingLocation && routingLocation.query) {
                const mailboxId = routingLocation.query['id'];
                if (mailboxId) {
                    this.props.setMessageAsReadAction(mailboxId);
                }
            }
        }
    }

    render() {
        const { fields } = this.state;
        return (
            <div className="container">
                <div className="messageReceivedHeader">
                    <h2>{fields.get('subject')}</h2>
                    <HVBButton
                        className="btn btn-primary"
                        type="button"
                        onClick={this.props.history.goBack}
                    >terug</HVBButton>
                </div>
                <div className="border-line"></div>
                <div className="two-clm">
                    <div className="row">
                        <div className="col-md-6">
                            <p>{fields.get('subject')}</p>
                        </div>
                        <div className="col-md-6 text-right">
                            <p>{fields.get('messageSendDateTime')}
                                <HVBButton
                                    className="hvb-button"
                                    extraClassName="p-button"
                                    type="button"
                                    onClick={() => this.props.history.push(`/messages/create?id=${this.props.routingLocation.query['id']}`)}
                                >Beantwoord</HVBButton>
                            </p>
                        </div>
                    </div>
                </div>


                <div>
                    {
                        fields.get('body')
                    }
                </div>
            </div>
        );
    }
}

export default connect(
    (state) => {
        return {
            message: messageReadSelector(state),
            routingLocation: routingLocationSelector(state),
        }
    },
    dispatch => bindActionCreators({
        fetchMessageWithId: fetchMessagesWithIdAction,
        clearMessageWithId: clearMessageWithIdAction,
        setMessageAsReadAction: setMessageAsReadAction,
    }, dispatch),
)(DetailMessage);
