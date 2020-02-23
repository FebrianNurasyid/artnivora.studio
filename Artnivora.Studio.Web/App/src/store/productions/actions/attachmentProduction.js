import { tokenKey } from 'hvb-shared-frontend/src/store/constants';
import { toast } from 'react-toastify';
import {
    uploadAttachmentProductionType,
} from '../../constants';

export const attachmentProductionAction = (formData) => async (dispatch) => {
    const tokenVar = sessionStorage.getItem(tokenKey);
    const url = `/api/Production/AddProductionAttachment`;    
    const response = await fetch(url, {
        method: 'POST',
        body: formData,
        headers: {
            'Authorization': 'Bearer ' + tokenVar
        }
    }).catch(error => { toast.error(error); });    
    const result = await response.json();
    if (response.ok) {
        dispatch({ type: uploadAttachmentProductionType, attachmentProduction: result })
    }
};
