import { fromJS } from 'immutable';
import {
    fetchUsersType,
    receiveUsersType
} from '../../constants';
import fetchWithBearerToken from 'hvb-shared-frontend/src/store/actions/fetchWithBearerToken';

export default () => async (dispatch, getState) => {
    dispatch({ type: fetchUsersType });

    const url = `/api/User`;
    const response = await fetchWithBearerToken(url,'GET');
    const users = await response.json();
    console.log('users', fromJS(users));
    dispatch({ type: receiveUsersType, users });
};