import { fromJS } from 'immutable';
import { tokenKey } from 'hvb-shared-frontend/src/store/constants';
import { toast } from 'react-toastify';
import {
    uploadAttachmentMessageType,
    //downloadAttachmentMessageType
} from '../../constants';

export const uploadAttachmentMessage = (formData) => async (dispatch) => {
    const tokenVar = sessionStorage.getItem(tokenKey);
    const url = `/api/Message/AddMessageAttachment`;
    const response = await fetch(url, {
        method: 'POST',
        body: formData,
        headers: {
            'Authorization': 'Bearer ' + tokenVar
        }
    }).catch(error => { toast.error(error); });
  
    const result = await response.json();
    if (response.ok) {
        dispatch({ type: uploadAttachmentMessageType, attachmentMessage: result })
    }
};

//export const downloadAttachmentMessage = (attachmentId) => async (dispatch) => {
//    const tokenVar = sessionStorage.getItem(tokenKey);
//    const url = `/api/Message/AddMessageAttachment`;
//    const response = await fetch(url, {
//        method: 'POST',
//        body: formData,
//        headers: {
//            'Authorization': 'Bearer ' + tokenVar
//        }
//    }).catch(error => { toast.error(error); });

//    const result = await response.json();
//    if (response.ok) {
//        dispatch({ type: uploadAttachmentMessageType, attachmentMessage: result })
//    }
//};

//export const downloadAttachmentMessage = (attachmentId) => (dispatch) => { 
//    const tokenVar = sessionStorage.getItem(tokenKey);
//    debugger;
//    return ({ fetch }) => ({
//        type: downloadAttachmentMessageType,
//        payload: {
//            promise: fetch(`/api/Message/GetMessageAttachmentById/${attachmentId}`, {
//                method: 'GET',
//                headers: {
//                    'Authorization': 'Bearer ' + tokenVar
//                }
//            }).then(function (response) {
//                return response.blob();
//            }).then(function (blob) {
//                download(blob);
//            })
//        }
//    });
//};
