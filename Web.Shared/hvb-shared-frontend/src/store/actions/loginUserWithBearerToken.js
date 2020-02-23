import { tokenKey, updateLoggedInUser, userId } from '../constants';

export default () => async (dispatch) => {
    const bearerToken = sessionStorage.getItem(tokenKey);
    if (!bearerToken) {
        return;
    }

    const url = `/api/Auth/AuthenticateWithBearerToken?bearerToken=${bearerToken}`;
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    });
    const result = await response.json();
    if (!response.ok) {
        sessionStorage.removeItem(tokenKey);
        sessionStorage.removeItem(userId);
    } else {
        sessionStorage.setItem(tokenKey, result.token);
        sessionStorage.setItem(userId, result.id);
        dispatch({ type: updateLoggedInUser, loggedInUser: result });
    }
};