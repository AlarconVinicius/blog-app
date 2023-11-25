import { Component, OnInit, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common'
import { AuthService } from 'src/app/shared/services/auth/auth.service';
import { Router } from '@angular/router';
import { AuthorResponse } from 'src/app/core/models/author/author.model';
import { UserService } from 'src/app/shared/services/user/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  user = {} as AuthorResponse;

  constructor(@Inject(DOCUMENT) private document: Document, private authService: AuthService, private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.getAuthUser();
    
  }
  public getAuthUser(){
    this.userService.getAuthUser().subscribe(data => {
      this.user = data;
    });
  }
  sidebarToggle()
  {
    //toggle sidebar function
    this.document.body.classList.toggle('toggle-sidebar');
  }
  
  logout(){
    this.authService.LocalStorage.clearLocalUserData();
    this.router.navigate(['']);
  }
}
