import { createSelector } from 'reselect';
import { Map } from 'immutable';

export const messagesStateSelector = state => state.messages;
export const linkInfoStateSelector = state => state.linkInfo;

export const messagesSelector = state => messagesStateSelector(state).messages;
export const linkInfoSelector = state => messagesStateSelector(state).linkInfo;

export const messageReadSelector = state => messagesStateSelector(state).message;

export const filteredMessagesSelector = createSelector(
    messagesSelector,
    (messages) => {
        return messages;
    }
);