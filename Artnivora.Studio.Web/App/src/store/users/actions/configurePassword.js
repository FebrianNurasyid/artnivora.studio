import { toast } from 'react-toastify';
import { tokenKey } from 'hvb-shared-frontend/src/store/constants';
import { encryptPassword } from 'hvb-shared-frontend/src/helpers/encryptionHelper';

export default (password) => async () => {   
    const tokenVar = sessionStorage.getItem(tokenKey);
    const url = `/api/User/configurepassword` + `?password=` + encryptPassword(password);
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + tokenVar
        }        
    });
    const result = await response;
    if (!response.ok) {
        toast.error("Configuration of password failed.", {
            position: toast.POSITION.TOP_RIGHT
        });        
    } else {
        toast.success("Password configuration success!", {
            position: toast.POSITION.TOP_RIGHT
        });        
    }
    console.log('result', result);
};