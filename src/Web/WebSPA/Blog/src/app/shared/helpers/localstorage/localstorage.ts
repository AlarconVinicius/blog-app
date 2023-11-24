import { ClaimsResponse, LoginResponse } from "src/app/core/models/auth/login.model";

export class LocalStorageUtils {

    public getUser() {
        return JSON.parse(localStorage.getItem('user') || '{}');
    }

    public saveLocalUserData(response: LoginResponse) {
        this.saveUserToken(response.accessToken);
        this.saveUserClaims(response.userToken.claims);
    }

    public clearLocalUserData() {
        localStorage.removeItem('token');
        localStorage.removeItem('claims');
    }

    public getUserToken(): string {
        return localStorage.getItem('token') || '';
    }
    public getUserClaims(): ClaimsResponse[] {
        const claimsString = localStorage.getItem('claims') || '[]';
        return JSON.parse(claimsString) as ClaimsResponse[];
    }

    public saveUserToken(token: string) {
        localStorage.setItem('token', token);
    }

    public saveUserClaims(claims: ClaimsResponse[]) {
        localStorage.setItem('claims', JSON.stringify(claims));
    }

    public hasWriterPermission(): boolean {
        const claims = this.getUserClaims();
        return claims.some(claim => claim.type === 'Permission' && claim.value.includes('Writer'));
    }

    public isLoggedIn(): boolean{
        if(Boolean(this.getUserToken())){
            return true;
        }
        return false
    }
}
