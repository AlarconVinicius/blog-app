import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';

@Injectable({
  providedIn: 'root'
})
export class IsAdminGuard implements CanActivate {
  private localStorageUtils = new LocalStorageUtils();

  constructor(private router: Router) {};

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      
      if (this.localStorageUtils.isAdmin()) {
        return true;
      } else {
        this.router.navigateByUrl(''); 
        return false;
      }
  }  
}
