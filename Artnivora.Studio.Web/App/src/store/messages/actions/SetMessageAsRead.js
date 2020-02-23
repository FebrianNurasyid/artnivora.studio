import { tokenKey } from 'hvb-shared-frontend/src/store/constants';

export default (mailBoxId) => async (dispatch, getState) => {

    const tokenVar = sessionStorage.getItem(tokenKey);
    const url = `/api/Message/SetMailBoxAsRead`;
    const response = await fetch(url, {
        method: 'POST',
        body: JSON.stringify({
            'MailboxId': mailBoxId
        }),
        headers: {
            'Authorization': 'Bearer ' + tokenVar,
            'Content-Type': 'application/json'
        }
    }).catch(error => { toast.error(error); });
}