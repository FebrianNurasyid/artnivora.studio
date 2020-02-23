import { fromJS } from 'immutable';
import {
    fetchUsersType,
    receiveUsersType
} from '../../constants';

export default () => async (dispatch, getState) => {
    dispatch({ type: fetchUsersType });

    const url = `/api/User`;
    const response = await fetch(url);
    const users = await response.json();
    console.log('users', fromJS(users));
    dispatch({ type: receiveUsersType, users });
};