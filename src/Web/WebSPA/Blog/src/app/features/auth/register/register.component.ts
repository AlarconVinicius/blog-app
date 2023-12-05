import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/shared/helpers/base-component/base-component';
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';
import { AuthService } from 'src/app/shared/services/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent extends BaseComponent implements OnInit {

  constructor(
    private titleService: Title,
    authService: AuthService,
    router: Router,
    localStorage: LocalStorageUtils) { super(router, localStorage, authService) }

  ngOnInit(): void {
    this.titleService.setTitle("Registrar | Receitas de Casal");
    this.isLoggedIn();
  }

}
