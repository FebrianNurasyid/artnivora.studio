import { tokenKey } from 'hvb-shared-frontend/src/store/constants';
import { toast } from 'react-toastify';
import { encryptPassword } from 'hvb-shared-frontend/src/helpers/encryptionHelper';
import {
    updateUserActivationState
} from '../../constants';

export default (entity, userroletype) => async (dispatch) => {    
    const tokenVar = sessionStorage.getItem(tokenKey);
    const url = `/api/User/Register`+`/?userroletype=`+userroletype;    
    const userEntity = {
        'Username': entity.username,
        'Email': entity.email,
        'Password': encryptPassword(entity.password),
        'Token': '',
    };    
    const userProfileEntity = {
        'Salutation': 'tes',
        'FirstName': entity.firstname,
        'LastName': entity.lastname,
        'Insertion': 'tes',
        'MaidenName': 'tes',        
        'MobileNumber': '0',
        'Birthdate': '1990-01-01',
        'PhoneNumber': entity.phonenumber,
        'ContactAddress': {
            'Street': 'tes',
            'HouseNumber': 'tes',
            'Zipcode': 'tes',
            'City': 'tes',
            'Country': 'Indonesia'
        }
    }    
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + tokenVar
        },
        body: JSON.stringify({
            User: userEntity,
            UserProfile: userProfileEntity,
        })
    });    
    if (response.ok) {            
        toast.success("Registration succesfull!", {
            position: toast.POSITION.TOP_RIGHT
        });
    } else {        
        toast.error("Registration Failed, Please contact administrator!", {
            position: toast.POSITION.TOP_RIGHT
        });
    }    
};