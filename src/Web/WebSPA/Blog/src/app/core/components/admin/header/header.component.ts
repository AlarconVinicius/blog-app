import { Component, OnInit, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common'
import { AuthService } from 'src/app/shared/services/auth/auth.service';
import { Router } from '@angular/router';
import { AuthorResponse } from 'src/app/core/models/author/author.model';
import { UserService } from 'src/app/shared/services/user/user.service';
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';
import { BaseComponent } from 'src/app/shared/helpers/base-component/base-component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent extends BaseComponent implements OnInit {
  user = {} as AuthorResponse;
  admin:boolean = false;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private userService: UserService, 
    router: Router, 
    localStorage: LocalStorageUtils,
    authService: AuthService) { super(router, localStorage, authService)}

  ngOnInit(): void {
    this.getAuthUser();
    this.admin = this.isAdmin();
    
  }
  public getAuthUser(){
    this.userService.getAuthUser().subscribe(data => {
      this.user = data;
    });
  }
  sidebarToggle()
  {
    this.document.body.classList.toggle('toggle-sidebar');
  }
  getImageUrl(base64: string) {
    return 'data:image/jpeg;base64,' + base64;
  }
}
