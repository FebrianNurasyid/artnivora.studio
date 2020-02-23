import { tokenKey } from '../constants';

export default async (url, method = 'GET', body = {}) => {
    const token = sessionStorage.getItem(tokenKey);

    let fetchArguments = {
        method: `${method}`,
        headers: {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        }
    };

    if (method === 'POST' || method === 'PUT') {
        fetchArguments.body = body;
    }

    const result = await fetch(url, fetchArguments);
    return result;
};