import { tokenKey, updateLoggedInUser } from '../constants';

export default () => async (dispatch) => {
    sessionStorage.removeItem(tokenKey);
    dispatch({ type: updateLoggedInUser, loggedInUser: null });
    window.location.href = "/users/login";
};