import {
    fetchUsersType,
    receiveUsersType,
    fetchUserToEditType,
    clearUserToEditType,
    updateUsersSearchQuery,
    updateUserActivationState,
    updatePictureTemp,
    userProfileData,
    updateUserProfilePicture,
    updateUserProfileState,
    getParticipantProfileData,
    getVolunteerProfileData,
    fetchUserDashboard,
    updateUserVolunteerProfileState
} from '../../constants';

const initialState = {
    users: [], userLoginDashboard: {}, isLoading: false
};

export default (state, action) => {
    state = state || initialState;

    if (action.type === fetchUsersType) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === receiveUsersType) {
        return {
            ...state,
            users: action.users,
            isLoading: false
        };
    }

    if (action.type === updateUsersSearchQuery) {
        return {
            ...state,
            searchQuery: action.searchQuery
        }
    }

    if (action.type === fetchUserToEditType) {
        return {
            ...state,
            userToEdit: action.userToEdit
        }
    }

    if (action.type === clearUserToEditType) {
        return {
            ...state,
            userToEdit: null,
        }
    }

    if (action.type === updateUserActivationState) {
        return {
            ...state,
            userActivated: action.userActivated,
            tokenValid: action.tokenValid,
        }
    }
    if (action.type === updateUserProfilePicture) {
        return {
            ...state,
            userProfilePicture: action.userProfilePicture
        }
    }

    if (action.type === fetchUserDashboard) {
        return {
            ...state,
            userLoginDashboard: action.userLoginDashboard
        }
    }

    //userProfileData
    
    if (action.type === updateUserProfileState) {

        return {
            ...state, userParticipantData: action.userProfileData
        }
    }

    if (action.type === getParticipantProfileData) {
        return {
            ...state, userParticipantData: action.userParticipantData
        }
    }

    if (action.type === getVolunteerProfileData) {
        return {
            ...state, userVolunteerData: action.userVolunteerData
        }
    }
    if (action.type === updateUserVolunteerProfileState) {
        
        return {
            ...state, volunteerUpdatedData: action.volunteerUpdatedData
        }
    }

    return state;
};
