import { fromJS } from 'immutable';
import { tokenKey } from 'hvb-shared-frontend/src/store/constants';
import { toast } from 'react-toastify';
import {
    updateUserProfilePicture,
    updateUserProfileState,
    getParticipantProfileData,
    getVolunteerProfileData,
    updateUserVolunteerProfileState
} from '../../constants';

export const uploadProfilePicture = (formData) => async (dispatch) => {
    const tokenVar = sessionStorage.getItem(tokenKey);
    const url = `/api/UserProfile/UploadProfilePicture`;
    const response = await fetch(url, {
        method: 'POST',
        body: formData,
        headers: {
            'Authorization': 'Bearer ' + tokenVar
        }
    }).catch(error => { toast.error(error); });

    if (response.ok) {
        await response.arrayBuffer().then((buffer) => {
            const base64Flag = 'data:image/jpeg;base64,';
            const imageStr = arrayBufferToBase64(buffer);
            dispatch({ type: updateUserProfilePicture, userProfilePicture: base64Flag + imageStr })
        });
        
    }
};

export const fetchProfileImage = () => async (dispatch) => {
    const tokenVar = sessionStorage.getItem(tokenKey);
    const url = `api/UserProfile/GetImage`;
    const response = await fetch(url, {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer ' + tokenVar
        }
    }).catch(error => { toast.error(error); });

    if (response.ok) {
        await response.arrayBuffer().then((buffer) => {
            const base64Flag = 'data:image/jpeg;base64,';
            const imageStr = arrayBufferToBase64(buffer);
            dispatch({ type: updateUserProfilePicture, userProfilePicture: base64Flag + imageStr })
        });
    }
}

function arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = [].slice.call(new Uint8Array(buffer));

    bytes.forEach((b) => binary += String.fromCharCode(b));

    return window.btoa(binary);
};

export const getParticipantProfile = () => async (dispatch) =>
{
    const tokenVar = sessionStorage.getItem(tokenKey);

    const url = `/api/UserProfile/GetParticipantProfile`;
    const response = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + tokenVar
        }
    }).catch(error => {  toast.error(error); });
    if (response.ok)
    {
        const responseBody = await response.json();
        
        const participantProfileData = ParticipantToImmutable(responseBody);
        dispatch({ type: getParticipantProfileData, userParticipantData: participantProfileData })
    }
}

export const saveUserProfile = (entity) => async (dispatch) => {
    const url = `/api/UserProfile/UploadParticipantProfile`;
    
    const address = {
        'street': entity.street,
        'zipcode': entity.zipcode,
        'city': entity.city,
        'housenumber': entity.housenumber
    }
    const participantProfileEntity = {
        'healthcareProviderId': entity.healthcareproviderid
    }
    const userProfileEntity = {
        'profilepicture': entity.profilepicture,
        'salutation': entity.salutation,
        'firstname': entity.firstname,
        'initial': entity.initial,
        'lastname': entity.lastname,
        'insertion': entity.insertion,
        'maidenname': entity.maidenname,
        'gender': entity.gender,
        'birthdate': entity.birthdate,
        'phonenumber': entity.phonenumber,
        'mobilenumber': entity.phonenumbermobile,
        'email': entity.email,
        'telephonelistagree': entity.telephonelistagree,
        'holidayweekagree': entity.holidayweekagree,
        'newsletter': entity.newsletter,
        'contactaddress':address
    }
    const tokenVar = sessionStorage.getItem(tokenKey);
    const userEntity = {
        'Username': entity.email,
        'Email': entity.email,
        'Token': `${tokenVar}`,
    };
    const result = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + tokenVar
        },
        body: JSON.stringify({
            user: userEntity,
            UserProfile: userProfileEntity,
            participantProfile: participantProfileEntity,
            isParticipant: true
        })
    }).then(res => {
        
        if (res.status == 200) return res;
        return null;
    }).catch(error => { toast.error(error); });
    if (result !== null) {
        
        const responseBody = await result.json();
        let participantProfileData = ParticipantToImmutable(responseBody);
        toast.success("Data Updated.", {
            position: toast.POSITION.TOP_RIGHT
        });
        dispatch({ type: updateUserProfileState, userProfileData: participantProfileData });
    };
    
};

export const getVolunteerProfile = () => async (dispatch) => {
    const tokenVar = sessionStorage.getItem(tokenKey);

    const url = `/api/UserProfile/GetVolunteerProfile`;
    const response = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + tokenVar
        }
    }).catch(error => { toast.error(error); });
    if (response.ok) {        
        const responseBody = await response.json();        
        
        const volunteerProfileData = volunteerToImmutable(responseBody);
        dispatch({ type: getVolunteerProfileData, userVolunteerData: volunteerProfileData })
    }
};


export const saveVolunteerProfile = (entity) => async (dispatch) => {
    
    const url = `/api/UserProfile/UploadParticipantProfile`;

    const address = {
        'street': entity.street,
        'zipcode': entity.zipcode,
        'city': entity.city,
        'housenumber': entity.housenumber,
        'country': entity.country
    }
    const volunteerProfileEntity = {
        'maritalStatus': parseInt(entity.maritalstatus),
        'targetAudiences': [],
        'volunteerFunctions':[]

    }
    const userProfileEntity = {
        'profilepicture': entity.profilepicture,
        'salutation': entity.salutation,
        'firstname': entity.firstname,
        'initial': entity.initial,
        'lastname': entity.lastname,
        'insertion': entity.insertion,
        'maidenname': entity.maidenname,
        'gender': entity.gender,
        'birthdate': entity.birthdate,
        'phonenumber': entity.phonenumber,
        'mobilenumber': entity.phonenumbermobile,
        'email': entity.email,
        'telephonelistagree': entity.telephonelistagree,
        'holidayweekagree': entity.holidayweekagree,
        'newsletter': entity.newsletter,
        'contactaddress': address
    }
    const tokenVar = sessionStorage.getItem(tokenKey);
    const userEntity = {
        'Username': entity.email,
        'Email': entity.email,
        'Token': `${tokenVar}`,
    };
    const result = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + tokenVar
        },
        body: JSON.stringify({
            user: userEntity,
            volunteerProfile: volunteerProfileEntity,
            userProfile: userProfileEntity,
        })
    }).then(res => {
        
        if (res.status == 200) return res;
        return null;
    }).catch(error => { toast.error(error); });
    if (result !== null) {
        const responseBody = await result.json();
        let volunteerProfileData = volunteerToImmutable(responseBody);
        toast.success("Data Updated.", {
            position: toast.POSITION.TOP_RIGHT
        });
        dispatch({ type: updateUserVolunteerProfileState, volunteerUpdatedData: volunteerProfileData });
    };
};

const ParticipantToImmutable = (responseBody) =>
{
    responseBody.userProfile.healthcareProviderId = responseBody.healthcareProviderId;
    return fromJS({
        salutation: responseBody.userProfile.salutation,
        firstname: responseBody.userProfile.firstName,
        initial: responseBody.userProfile.initial,
        lastname: responseBody.userProfile.lastName,
        insertion: responseBody.userProfile.insertion,
        maidenname: responseBody.userProfile.maidenName,
        gender: responseBody.userProfile.gender,
        birthdate: new Date(responseBody.userProfile.birthdate),
        healthcareproviderid: responseBody.healthcareProviderId,
        street: responseBody.userProfile.contactAddress.street,
        housenumber: responseBody.userProfile.contactAddress.houseNumber,
        zipcode: responseBody.userProfile.contactAddress.zipCode,
        city: responseBody.userProfile.contactAddress.city,
        phonenumber: responseBody.userProfile.phoneNumber,
        phonenumbermobile: responseBody.userProfile.mobileNumber,
        email: responseBody.userProfile.user.email,
        telephonelistagree: responseBody.userProfile.telephoneListAgree,
        holidayweekagree: responseBody.userProfile.holidayWeekAgree,
        profilePicture: responseBody.userProfile.profilePicture,
        newsletter: responseBody.userProfile.newsletter,
        country: responseBody.userProfile.contactAddress.country
    });
    return participantProfileData;
}
const volunteerToImmutable = (responseBody) => {
    return fromJS({
        salutation: responseBody.userProfile.salutation,
        firstname: responseBody.userProfile.firstName,
        initial: responseBody.userProfile.initial,
        lastname: responseBody.userProfile.lastName,
        insertion: responseBody.userProfile.insertion,
        maidenname: responseBody.userProfile.maidenName,
        gender: responseBody.userProfile.gender,
        birthdate: new Date(responseBody.userProfile.birthdate),
        healthcareproviderid: responseBody.healthcareProviderId,
        street: responseBody.userProfile.contactAddress.street,
        housenumber: responseBody.userProfile.contactAddress.houseNumber,
        zipcode: responseBody.userProfile.contactAddress.zipCode,
        city: responseBody.userProfile.contactAddress.city,
        phonenumber: responseBody.userProfile.phoneNumber,
        phonenumbermobile: responseBody.userProfile.mobileNumber,
        email: responseBody.userProfile.user.email,
        telephonelistagree: responseBody.userProfile.telephoneListAgree,
        holidayweekagree: responseBody.userProfile.holidayWeekAgree,
        profilePicture: responseBody.userProfile.profilePicture,
        newsletter: responseBody.userProfile.newsletter,
        maritalstatus: responseBody.maritalStatus,
        country: responseBody.userProfile.contactAddress.country
    });
}