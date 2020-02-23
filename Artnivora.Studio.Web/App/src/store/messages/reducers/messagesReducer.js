import {
    fetchMessagesType,
    receiveMessagesType,
    fetchMessageToReadType,
    clearMessageToReadType,
    uploadAttachmentMessageType,
} from '../../constants';

import { Map } from 'immutable';

const initialState = { messages: Map(), isLoading: false };

export default (state, action) => {
    state = state || initialState;

    if (action.type === fetchMessagesType) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === receiveMessagesType) {
        return {
            ...state,
            messages: action.messages,
            linkInfo: action.linkInfo,
            isLoading: false
        };
    }

    if (action.type === fetchMessageToReadType) {
        return {
            ...state,
            message: action.message            
        };
    }


    if (action.type === clearMessageToReadType) {
        return {
            ...state,
            message: null,
        }
    }

    if (action.type === uploadAttachmentMessageType) {
        return {
            ...state,
            attachmentMessage: action.attachmentMessage
        }
    }

    return state;
};
