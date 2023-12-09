import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';
import { LoginRequest, LoginResponse } from 'src/app/core/models/auth/login.model';
import { AuthService } from 'src/app/shared/services/auth/auth.service';
import { Title } from '@angular/platform-browser';
import { BaseComponent } from 'src/app/shared/helpers/base-component/base-component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent extends BaseComponent implements OnInit {
  tokenExpiration: Date | null = null;
  loginObj: LoginRequest = {} as LoginRequest;
  loginResponse = {} as LoginResponse;
  constructor(
    private titleService: Title,
    authService: AuthService, 
    router: Router,
    localStorage: LocalStorageUtils) { super(router, localStorage, authService)}

  ngOnInit(): void {
    this.titleService.setTitle("Login | Receitas de Casal");
    this.isLoggedIn();
  }

  onLogin() {
    this.authService.login(this.loginObj).subscribe(data => {
      this.loginResponse = data
      this.authService.localStorage.saveLocalUserData(this.loginResponse);
      if(Boolean(this.authService.localStorage.getUserToken)){
        this.router.navigate(['admin']);
      }
      this.tokenExpiration = new Date(Date.now()); 
      this.tokenExpiration.setUTCSeconds(this.loginResponse.expiresIn);
      this.authService.localStorage.saveUserTokenExpire(this.tokenExpiration)
    });
  }
}
