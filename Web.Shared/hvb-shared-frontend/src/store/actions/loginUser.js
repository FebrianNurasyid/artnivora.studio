import { toast } from 'react-toastify';
import { tokenKey, updateLoggedInUser, userId } from '../constants';
import { encryptPassword } from '../../helpers/encryptionHelper';

export default (loginData) => async (dispatch) => {
    const url = `/api/Auth/Authenticate`;
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            'username': loginData.username,
            'password': encryptPassword(loginData.password), 
        })
    });
    const result = await response.json();
    if (!response.ok) {
        toast.error("Login failed.", {
            position: toast.POSITION.TOP_RIGHT
        });
        sessionStorage.removeItem(tokenKey);
        sessionStorage.removeItem(userId);
    } else {
        toast.success("Logged in.", {
            position: toast.POSITION.TOP_RIGHT
        });
        sessionStorage.setItem(tokenKey, result.token);
        sessionStorage.setItem(userId, result.id);
        dispatch({ type: updateLoggedInUser, loggedInUser: result });
        //window.location.href = "/users/userprofile";
    }
    console.log('result', result);
};