import { toast } from 'react-toastify';
import { encryptPassword } from 'hvb-shared-frontend/src/helpers/encryptionHelper';
import {
    updateUserActivationState
} from '../../constants';

export default (entity, userroletype) => async (dispatch) => {
    const url = `/api/User/Register`+`/?userroletype=`+userroletype;
    const shouldEncryptPassword = entity.password && entity.password !== '' && entity.password.length > 1;
    const userEntity = {
        'Username': entity.email,
        'Email': entity.email,
        'Password': shouldEncryptPassword ? encryptPassword(entity.password) : '',
        'Token': '',
    };

    const userProfileEntity = {
        'Salutation': entity.salutation,
        'FirstName': entity.firstname,
        'LastName': entity.lastname,
        'Insertion': entity.insertion,
        'MaidenName': entity.maidenname,
        'Birthdate': entity.birthdate,
        'MobileNumber': entity.phonenumbermobile,
        'PhoneNumber': entity.phonenumber,
        'ContactAddress': {
            'Street': entity.street,
            'HouseNumber': entity.housenumber,
            'Zipcode': entity.zipcode,
            'City': entity.city,
            'Country': 'Nederland'
        }
    }

    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            User: userEntity,
            UserProfile: userProfileEntity,
        })
    });
    const result = await response.json();
    console.log('response! ' + response);
    console.log("result " + result);
    if (response.ok) {
        // Set user as activation state to null in the local state, because it has not passed or failed yet
        dispatch({ type: updateUserActivationState, userActivated: null });
        window.location.href = "/users/thankyou";
        toast.success("Registration succesfull!", {
            position: toast.POSITION.TOP_RIGHT
        });
    } else {
        // I use the errors returned from the back-end to show a message
        toast.error("Username is already in use!", {
            position: toast.POSITION.TOP_RIGHT
        });
    }
    console.log('result', result);
};