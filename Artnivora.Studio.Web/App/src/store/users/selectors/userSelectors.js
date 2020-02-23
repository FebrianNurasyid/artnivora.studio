import { createSelector } from 'reselect';

export const usersStateSelector = state => state.users;
export const activationStateSelector = state => state.users.userActivated;
export const validTokenStateSelector = state => state.users.tokenValid;

export const usersSelector = state => usersStateSelector(state).users;
export const userToEditSelector = state => usersStateSelector(state).userToEdit;

export const searchQuerySelector = state => usersStateSelector(state).searchQuery;

export const userDashboardSelector = state => state.users.userLoginDashboard || {};

export const filteredUsersSelector = createSelector(
    usersSelector, 
    searchQuerySelector, 
    (users, searchQuery) => {
        if (!searchQuery) {
            return users;
        }

        return users.filter(data =>
            data.username.includes(searchQuery) ||
            data.id.includes(searchQuery)
        );
    }
);