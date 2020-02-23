import {
    fetchMessagesType,
    receiveMessagesType
} from '../../constants';
import { normalize } from 'hvb-shared-frontend/src/helpers/normalizeHelper';
import fetchWithBearerToken from 'hvb-shared-frontend/src/store/actions/fetchWithBearerToken';
import { fromJS } from 'immutable';

export default (page,count,urlToCall) => async (dispatch, getState) => {
    dispatch({ type: fetchMessagesType });

    let url;
    if (urlToCall) {
        url = urlToCall;
    }
    else
        url = `/api/Message/inbox?offset=${ page??0 }&limit=${ count??10 }`;

    const response = await fetchWithBearerToken(url);

    const result = await response.json();
    const messages = fromJS(result.messages);
    const itemMsg = messages.map(item => { return item.set('isChecked', false) });

    if (response.ok) {
        dispatch({ type: receiveMessagesType, messages: normalize(itemMsg, 'id'), linkInfo:result.linkInfo });
    }

};