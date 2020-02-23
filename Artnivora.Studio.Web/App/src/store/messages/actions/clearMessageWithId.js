import {
    clearMessageToReadType
} from '../../constants';

export default () => async (dispatch) => {
    dispatch({ type: clearMessageToReadType });
};