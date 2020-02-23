import { createSelector } from 'reselect';
import { tokenKey } from '../constants';

export const authSelector = state => state.auth;
export const loggedInUserSelector = state => authSelector(state).loggedInUser;

export const userLoggedInSelector = createSelector(
    loggedInUserSelector,
    (loggedInUser) => {
        if (loggedInUser && loggedInUser.token) {
            return true;
        }

        return false;
    }
);