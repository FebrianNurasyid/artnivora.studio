import {
    fetchProdcutionToEditType
} from '../../constants';
import fetchWithBearerToken from 'hvb-shared-frontend/src/store/actions/fetchWithBearerToken';

export default (prodId) => async (dispatch, getState) => {
    const url = `/api/Production/GetProdById/${prodId}`;
    const response = await fetchWithBearerToken(url);

    const result = await response.json();
    if (response.ok) {
        dispatch({ type: fetchProdcutionToEditType, prodToEdit: result });
    }
};