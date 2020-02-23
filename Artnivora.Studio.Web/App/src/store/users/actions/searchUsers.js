import {
    updateUsersSearchQuery
} from '../../constants';

export default (searchQuery, name) => (dispatch) => {
    dispatch({ type: updateUsersSearchQuery, searchQuery: searchQuery, name: name });
}