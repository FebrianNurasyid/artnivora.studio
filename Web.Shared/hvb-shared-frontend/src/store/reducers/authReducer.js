import {
    updateLoggedInUser,
} from '../constants';

const initialState = { };

export default (state, action) => {
    state = state || initialState;

    if (action.type === updateLoggedInUser) {
        return {
            ...state,
            loggedInUser: action.loggedInUser,
        };
    }

    return state;
};
