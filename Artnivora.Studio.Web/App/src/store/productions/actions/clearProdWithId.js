import {
    clearProdToEditType
} from '../../constants';

export default () => async (dispatch) => {
    dispatch({ type: clearProdToEditType });
};