import fetchWithBearerToken from 'hvb-shared-frontend/src/store/actions/fetchWithBearerToken';

import { fetchUserDashboard, userId } from '../../constants';
import { toast } from 'react-toastify';

export default () => async (dispatch) => {
    dispatch({ type: fetchUserDashboard });
    const userLoginId = sessionStorage.getItem(userId);
    const url = `/api/dashboard/ByUserId/${userLoginId}`;
    
    const response = await fetchWithBearerToken(url, 'GET');
    if (!response.ok) {
        toast.error("Request failed.", {
            position: toast.POSITION.TOP_RIGHT
        });
    } else {
        const jsonResult = await response.json();
        dispatch({ type: fetchUserDashboard, userLoginDashboard : jsonResult });
    }
};