import * as CryptoJS from 'crypto-js';

export class EncryptionService {
    public static encrypt(token: string, request: any): string {
        const key = CryptoJS.enc.Utf8.parse(token);
        const ivParam = CryptoJS.enc.Utf8.parse(token);
        const encrypted = CryptoJS.AES.encrypt(
            JSON.stringify(request), key, {
            keySize: 16,
                iv: ivParam,
            mode: CryptoJS.mode.ECB,
            padding: CryptoJS.pad.Pkcs7
        });
        return encrypted.toString();
    }
    public static decrypt<T>(token: string, encryptedValue: string): T {
        const key = CryptoJS.enc.Utf8.parse(token);
        const ivParam = CryptoJS.enc.Utf8.parse(token);

        const data = CryptoJS.AES.decrypt(
            encryptedValue, key, {
            keySize: 16,
            iv: ivParam,
            mode: CryptoJS.mode.ECB,
            padding: CryptoJS.pad.Pkcs7
        }).toString(CryptoJS.enc.Utf8);
        return JSON.parse(data) as T;
    }
}
