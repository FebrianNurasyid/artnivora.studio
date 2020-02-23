import { toast } from 'react-toastify';

export default (userMail) => async () => {
    const url = `/api/User/ForgotPassword` + `?email=` + userMail;
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    });    
    if (response.ok) {
        toast.success("Er is een email verstuurd naar " + userMail + ", open uw mailbox om uw wachtwoord te herstellen", {
            position: toast.POSITION.TOP_CENTER
        });
    }
    else {
        const result = await response.json();
        const allErrors = result.errors.join(',');
        toast.error(`${allErrors}`, {
            position: toast.POSITION.TOP_CENTER
        });        
    }
};