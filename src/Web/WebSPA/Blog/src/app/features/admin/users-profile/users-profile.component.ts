import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorRequest, AuthorResponse } from 'src/app/core/models/author/author.model';
import { UserService } from 'src/app/shared/services/user/user.service';

@Component({
  selector: 'app-users-profile',
  templateUrl: './users-profile.component.html',
  styleUrls: ['./users-profile.component.css']
})
export class UsersProfileComponent implements OnInit {
  user = {} as AuthorResponse;
  userInput = {} as AuthorRequest;
  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    this.getAuthUser();
    
  }
  public getAuthUser(){
    this.userService.getAuthUser().subscribe(data => {
      console.log(data)
      this.user = data;
      this.userInput = data;
    });
  }
  public putAuthUser(){
    this.userService.putAuthUser(this.userInput).subscribe(_ => {
      this.getAuthUser();
      this.router.navigate(['admin/perfil']);
    });
  }
}
