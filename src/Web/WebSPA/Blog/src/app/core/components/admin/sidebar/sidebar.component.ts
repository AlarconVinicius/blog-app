import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/shared/helpers/base-component/base-component';
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';
import { AuthService } from 'src/app/shared/services/auth/auth.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent extends BaseComponent implements OnInit {

  admin:boolean = false;
  constructor(
    router: Router,
    localStorage: LocalStorageUtils,
    authService: AuthService) { super(router, localStorage, authService)}

  ngOnInit(): void {
    this.admin = this.isAdmin();
  }

}
