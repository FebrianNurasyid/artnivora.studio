import {
    clearUserToEditType
} from '../../constants';

export default () => async (dispatch) => {
    dispatch({ type: clearUserToEditType });
};