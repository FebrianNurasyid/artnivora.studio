import { toast } from 'react-toastify';
import fetchWithBearerToken from 'hvb-shared-frontend/src/store/actions/fetchWithBearerToken';

export default () => async () => {
    const response = await fetchWithBearerToken(`/api/Auth`, 'GET');
    if (!response.ok) {
        toast.error("Request failed.", {
            position: toast.POSITION.TOP_RIGHT
        });
    } else {
        const jsonResult = await response.json();
        console.log('jsonResult', jsonResult);
        toast.success("Request passed.", {
            position: toast.POSITION.TOP_RIGHT
        });
    }
};