import {
    fetchMessageToReadType
} from '../../constants';
import { normalize } from 'hvb-shared-frontend/src/helpers/normalizeHelper';
import fetchWithBearerToken from 'hvb-shared-frontend/src/store/actions/fetchWithBearerToken';

export default (mailBoxId) => async (dispatch, getState) => {

    const url = `/api/Message/GetMessageById/${mailBoxId}`;
    const response = await fetchWithBearerToken(url);

    const result = await response.json();
    if (response.ok) {
        dispatch({ type: fetchMessageToReadType, message: result });
    }
}