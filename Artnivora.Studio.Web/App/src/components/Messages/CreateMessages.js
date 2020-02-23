import { fromJS } from 'immutable';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';
import './MessageReceived.css';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';
import clearMessageWithIdAction from '../../store/messages/actions/clearMessageWithId';
import fetchMessagesWithIdAction from '../../store/messages/actions/fetchMessageWithId';
import { messageReadSelector } from '../../store/messages/selectors/messagesSelectors';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import 'font-awesome/css/font-awesome.min.css';
import ReactSummernote from 'react-summernote';
import 'react-summernote/dist/react-summernote.css';
require('bootstrap');
import { uploadAttachmentMessage } from '../../store/messages/actions/attachmentMessage';
import { fileSizeLimit, fileFormatAllowed } from '../../constants/fileConstants';
import { toast } from 'react-toastify';

class CreateMessages extends Component {
    constructor(props) {
        super(props)
        this.state = {
            fields: fromJS({
                body: '',
                subject: '',                
                id: '',
                from: '',
                attachmentid:''
            }),
            attachment: fromJS({
                id: '',
                filename: '',
                filepath: '',
            })
        };

        this.onAttachmentChangeHandler = this.onAttachmentChangeHandler.bind(this);
    }

    componentDidMount() {
        const {
            routingLocation
        } = this.props;

        this.props.clearMessageWithId();
        if (routingLocation && routingLocation.query) {
            const msgId = routingLocation.query['id'];
            if (msgId) {
                this.props.fetchMessageWithId(msgId);
            }
        }

    }

    static getDerivedStateFromProps(props, state) {
        if (state && state.fields) {
            if (props && props.message)
                return {
                    fields: fromJS({
                        body: props.message.body,
                        subject: props.message.subject,
                        messageSendDateTime: props.message.messageSendDateTime,
                        senderId: props.message.senderId,
                        from: props.message.from,
                    })
                }
        }
        else
            return null;        
    }

    componentDidUpdate(prevProps) {        
        const { attachmentMessage } = this.props;        
        if (attachmentMessage !== prevProps.attachmentMessage) {
            if (attachmentMessage) {
                this.setState({
                    attachment: fromJS({
                        id: attachmentMessage.id,
                        filename: attachmentMessage.fileName,
                        filepath: attachmentMessage.path,
                    }),
                })
            }
        }
    }

    onAttachmentChangeHandler = e => {
        const files = e.target.files;

        let formData = new FormData();
        let fileType = '';

        if (files[0] !== null)
            fileType = files[0].type;

        if (files[0].size > fileSizeLimit) {
            toast.error("This file size is not allowed", {
                position: toast.POSITION.TOP_CENTER
            });
            return;
        }
        if (fileFormatAllowed.indexOf(fileType) == -1) {
            toast.error("This file format is not allowed", {
                position: toast.POSITION.TOP_CENTER
            });
            return;
        }

        formData.append('file', files[0]);

        this.props.uploadAttachmentMessage(formData);
    }

    render() {
        const { fields, attachment } = this.state;
        debugger;
        return (
            <div className="messageReceivedContainer" >
                <h2>Nieuw bericht</h2>
                <div className="border-line"></div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label" htmlFor="to_field">Aan</label>
                    <div className="col-md-6">
                        <TextInputField
                            field={'from'}
                            value={fields.get('from')}
                        />
                    </div>
                </div>
                <div className="form-group row required">
                    <label className="col-sm-2 col-form-label" htmlFor="subject_field">Onderwerp</label>
                    <div className="col-md-6">
                        <TextInputField
                            field={'subject'}
                            value={`RE: ${fields.get('subject')}`}
                        />
                        <div>&nbsp;</div>
                        <span className="btn btn-file btn-file-upload attachment-button">
                            Bijlage toevoegen &nbsp; <i className="fa fa-paperclip" aria-hidden="true"></i> <input type="file" name="file" onChange={this.onAttachmentChangeHandler} />
                        </span>     
                        <div>&nbsp;</div>
                        <a download href={attachment.get('filename')}>
                            {` ${attachment.get('filename')}`}
                        </a>
                    </div>
                </div>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label" htmlFor="body_field"></label>
                    <div className="col-md-10">
                        <ReactSummernote
                            value={`<br/> <hr/> Subject: ${fields.get('subject')} <br/> From: ${fields.get('from')} <br/> Sent: ${fields.get('messageSendDateTime')} <br/> ${fields.get('body')}`}
                            field={'body'}
                            options={{
                                height: 350,
                                dialogsInBody: true,
                                toolbar: [
                                    ['style', ['style']],
                                    ['font', ['bold', 'underline', 'clear']],
                                    ['fontname', ['fontname']],
                                    ['para', ['ul', 'ol', 'paragraph']],
                                    ['table', ['table']], ['lineHeights', ['0.2', '0.3', '0.4', '0.5', '0.6', '0.8', '1.0', '1.2', '1.4', '1.5', '2.0', '3.0']]
                                ], popover: []
                            }}
                        />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label"></label>
                    <div className="col-md-6">
                        <HVBButton
                            className="btn btn-primary"
                            type="button"
                        >Sturen</HVBButton>
                    </div>
                </div>

            </div >
        );
    }
}

export default connect(
    (state) => {
        return {
            message: messageReadSelector(state),
            routingLocation: routingLocationSelector(state),
            loggedInUser: state.auth.loggedInUser,
            attachmentMessage: state.messages.attachmentMessage
        }
    },
    dispatch => bindActionCreators({
        fetchMessageWithId: fetchMessagesWithIdAction,
        clearMessageWithId: clearMessageWithIdAction,
        uploadAttachmentMessage,
    }, dispatch),
)(CreateMessages);

