
import CryptoJS from 'crypto-js';

export const encryptPassword = (password) => {
    return CryptoJS.SHA256(password).toString()
}
