import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorRequest, AuthorResponse, UserPasswordRequest } from 'src/app/core/models/author/author.model';
import { ImageRequest } from 'src/app/core/models/image/image.model';
import { UserService } from 'src/app/shared/services/user/user.service';

@Component({
  selector: 'app-users-profile',
  templateUrl: './users-profile.component.html',
  styleUrls: ['./users-profile.component.css']
})
export class UsersProfileComponent implements OnInit {
  uploadImage = {} as ImageRequest;
  user = {} as AuthorResponse;
  userInput = {} as AuthorRequest;
  userPasswordInput = {} as UserPasswordRequest;
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
  getImageUrl(base64: string) {
    return 'data:image/jpeg;base64,' + base64;
  }
  upload(event: any) {
    const file = event.target.files[0];
    const name = event.target.files[0].name;
    const reader = new FileReader();

    reader.onload = () => {
        if (typeof reader.result === 'string') {
            let base64Image = reader.result as string;
            
            if (base64Image.startsWith('data:image/jpeg;base64,')) {
                base64Image = base64Image.replace('data:image/jpeg;base64,', '');
            }
            
            this.uploadImage.file = base64Image;
            this.uploadImage.name = name;
        } else {
            console.error('Não foi possível ler o arquivo como uma string.');
        }
    };
    if (file) {
      reader.readAsDataURL(file);
  }
  }
  public putAuthUser(){
    this.userInput.profileImage = this.uploadImage;
    console.log(this.userInput)
    this.userService.putAuthUser(this.userInput).subscribe(_ => {
      this.getAuthUser();
      this.router.navigateByUrl('/',{skipLocationChange:true}).then(()=>{
        this.router.navigate(['admin/perfil']).then(()=>{});
      });
    });
  }

  public putChangeUserPassword(){
    this.userService.putChangeUserPassword(this.userPasswordInput).subscribe(_ => {
      this.getAuthUser();
      this.router.navigate(['admin/perfil']);
      this.userPasswordInput.oldPassword = '';
      this.userPasswordInput.newPassword = '';
      this.userPasswordInput.confirmNewPassword = '';
    });
  }
}
