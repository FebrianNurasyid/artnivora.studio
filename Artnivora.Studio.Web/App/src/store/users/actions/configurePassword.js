import { toast } from 'react-toastify';
import { tokenKey } from 'hvb-shared-frontend/src/store/constants';
import { encryptPassword } from 'hvb-shared-frontend/src/helpers/encryptionHelper';

export default (password, activationToken, isrecoverypassword) => async () => {
    const url = `/api/User/configurepassword` + `?isrecoverypassword=` + isrecoverypassword;

    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            'Password': encryptPassword(password), 
            'Activation_Token': activationToken,
        })
    });
    const result = await response;

    if (!response.ok) {
        toast.error("Configuration of password failed.", {
            position: toast.POSITION.TOP_RIGHT
        });
        sessionStorage.removeItem(tokenKey);
    } else {
        toast.success("Password configuration success!", {
            position: toast.POSITION.TOP_RIGHT
        });

        window.location.href = "/users/login";
    }
    console.log('result', result);
};