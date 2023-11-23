import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { LocalStorageUtils } from '../helper/localstorage/localstorage';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  private localStorageUtils = new LocalStorageUtils();

  constructor(private router: Router) {};

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
    if(!Boolean(this.localStorageUtils.getUserToken())){
        this.router.navigateByUrl('auth/login')
        return false;
    }
    return true;
  }
  
}
