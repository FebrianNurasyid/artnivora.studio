import {
    fetchUserToEditType
} from '../../constants';

export default (userId) => async (dispatch, getState) => {
    const url = `/api/User/ById/${userId}`;
    const response = await fetch(url);

    const result = await response.json();
    if (response.ok) {
        dispatch({ type: fetchUserToEditType, userToEdit: result });
    }
};