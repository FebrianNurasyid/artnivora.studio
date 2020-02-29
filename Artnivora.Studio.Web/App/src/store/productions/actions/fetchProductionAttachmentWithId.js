import { tokenKey } from 'hvb-shared-frontend/src/store/constants';

export default (attachmentProduction, type) => async () => {

    if (attachmentProduction) {
        const token = sessionStorage.getItem(tokenKey);
        const urlApi = `/api/Production/GetProductionAttachmentById/${attachmentProduction.id}?type=${type}`;
        var filename ='';

        if (attachmentProduction.fileName)
            filename = `${attachmentProduction.fileName}`;
        else
            filename = `${attachmentProduction.filename}`;

        fetch(urlApi, {
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        }).then(response => {
            response.blob().then(blob => {
                let url = window.URL.createObjectURL(blob);
                let a = document.createElement('a');
                a.href = url;
                a.download = filename;
                a.click();
            });
        });
    }
}