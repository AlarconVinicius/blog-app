import { Injectable } from "@angular/core";
import { ClaimsResponse, LoginResponse } from "src/app/core/models/auth/login.model";

@Injectable({
    providedIn: 'root'
})
export class LocalStorageUtils {

    public getUser() {
        return JSON.parse(localStorage.getItem('user') || '{}');
    }

    public saveLocalUserData(response: LoginResponse) {
        this.saveUserToken(response.accessToken);
        this.saveUserId(response.userToken.id);
        this.saveUserClaims(response.userToken.claims);
    }

    public clearLocalUserData() {
        localStorage.removeItem('id');
        localStorage.removeItem('token');
        localStorage.removeItem('expiresIn');
        localStorage.removeItem('claims');
    }

    public getUserToken(): string {
        return localStorage.getItem('token') || '';
    }

    public getUserTokenExpire(): number | null {
        const expirationTime = localStorage.getItem('expiresIn');
        return expirationTime ? parseInt(expirationTime, 10) : null;
    }

    public getUserId(): string {
        return localStorage.getItem('id') || '';
    }

    public getUserClaims(): ClaimsResponse[] {
        const claimsString = localStorage.getItem('claims') || '[]';
        return JSON.parse(claimsString) as ClaimsResponse[];
    }

    public saveUserToken(token: string) {
        localStorage.setItem('token', token);
    }

    public saveUserTokenExpire(expiresIn: Date) {
        const expirationTime = expiresIn.getTime();
        localStorage.setItem('expiresIn', expirationTime.toString());
      }

    public saveUserId(id: string) {
        localStorage.setItem('id', id);
    }

    public saveUserClaims(claims: ClaimsResponse[]) {
        localStorage.setItem('claims', JSON.stringify(claims));
    }

    public hasWriterPermission(): boolean {
        const claims = this.getUserClaims();
        return claims.some(claim => claim.type === 'Permission' && claim.value.includes('Writer'));
    }

    public isAdmin(): boolean {
        const claims = this.getUserClaims();
        return claims.some(claim => claim.type === 'role' && claim.value.includes('Admin'));
    }
    
    public isLoggedIn(): boolean {
        if (Boolean(this.getUserToken())) {
            return true;
        }
        return false
    }

    isTokenExpired(): boolean {
        const expirationTime = this.getUserTokenExpire();
        if (!expirationTime) {
          return true;
        }
        return expirationTime < new Date().getTime();
    }
}
