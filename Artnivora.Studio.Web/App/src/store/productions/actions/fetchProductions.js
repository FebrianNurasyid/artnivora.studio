import { fromJS } from 'immutable';
import fetchWithBearerToken from 'hvb-shared-frontend/src/store/actions/fetchWithBearerToken';
import {
    fetchProductionsType,
} from '../../constants';

export default (filtered) => async (dispatch, getState) => {    
    const url = `/api/Production/GetProductions/${filtered}`;    
    const response = await fetchWithBearerToken(url);    
    const production = await response.json();
    dispatch({ type: fetchProductionsType, productions: production.productions});
};