import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/shared/helpers/base-component/base-component'; // Corrigindo o caminho
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';
import { AuthService } from 'src/app/shared/services/auth/auth.service';

@Component({
  selector: 'app-site-header',
  templateUrl: './site-header.component.html',
  styleUrls: ['./site-header.component.css']
})
export class SiteHeaderComponent extends BaseComponent implements OnInit {
  admin: boolean = false;
  loggedIn: boolean = false;

  constructor(
    router: Router,
    localStorage: LocalStorageUtils,
    authService: AuthService
  ) {
    super(router, localStorage, authService);
  }

  ngOnInit() {
    this.admin = this.isAdmin();
    this.loggedIn = this.isLoggedIn();
  }

}
