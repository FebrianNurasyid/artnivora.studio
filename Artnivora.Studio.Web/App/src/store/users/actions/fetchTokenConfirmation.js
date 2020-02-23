import { toast } from 'react-toastify';
import {
    updateUserActivationState
} from '../../constants';

export default (token, isrecoverypassword) => async (dispatch, getState) => {
    const url = `/api/User/TokenConfirmation?token=` + `${token}` + `&` + `isrecoverypassword=` + `${isrecoverypassword}`;
    const response = await fetch(url);    
    if (response.ok) {
        const isValidToken = await response.json();
        if (!isrecoverypassword) {
            toast.success("Your account has been activated successfully.", {
                position: toast.POSITION.TOP_CENTER
            });
        }
        // Set user as activated in local state
        dispatch({ type: updateUserActivationState, userActivated: true, tokenValid: isValidToken });

        // This timeout is here so that you can see the activated message on the page
        setTimeout(() => {
            if (isrecoverypassword && isValidToken === true)
                window.location.href = `/configurepassword?recoverytoken=` + `${token}`;
            else if (!isrecoverypassword)
                window.location.href = `/configurepassword?activatetoken=` + `${token}`;
        }, 1000);
    }
    else {
        if (!isrecoverypassword) {
            toast.error("Something went wrong while activating your account.", {
                position: toast.POSITION.TOP_CENTER
            });
        }
        // Set user as unactivated in local state
        dispatch({ type: updateUserActivationState, userActivated: false, tokenValid: false });
    }
};