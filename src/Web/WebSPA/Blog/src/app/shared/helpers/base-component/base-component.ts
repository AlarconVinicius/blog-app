import { Router } from '@angular/router';
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';
import { AuthService } from '../../services/auth/auth.service';

export abstract class BaseComponent {
  constructor(
    protected router: Router, 
    protected localStorage: LocalStorageUtils,
    protected authService: AuthService
  ) {}

  isLoggedIn() {
    return this.localStorage.isLoggedIn();
  }

  logout(): void {
    this.authService.localStorage.clearLocalUserData();
    this.router.navigate(['']);
  }

  isAdmin(): boolean {
    return this.localStorage.isAdmin();
  }
}
