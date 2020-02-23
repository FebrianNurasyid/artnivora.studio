import { tokenKey } from 'hvb-shared-frontend/src/store/constants';
import { toast } from 'react-toastify';

export const saveProduction = (prod, attachment) => async () => {
    const tokenVar = sessionStorage.getItem(tokenKey);
    const url = `/api/Production/saveproduction`;    
    const production =
    {
        "Title": prod.title,
        "Category": prod.category,
        "Themes": prod.themes,
        "Concept": prod.concept,
        "Status": prod.status,
        "ProductionAttachments": [
            {
                "ProductionAttachementId": attachment.id
            }
        ]
    };   
    const result = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + tokenVar
        },
        body: JSON.stringify(production)
    }).then(res => {
        if (res.status == 200) return res;
        return null;
    }).catch(error => { toast.error(error); });
    if (result !== null) {

        toast.success("Data production saved successfully", {
            position: toast.POSITION.TOP_RIGHT
        });                
        return true;
    };
};