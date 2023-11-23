import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Login, LoginResponse } from 'src/app/models/auth/login';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginObj: Login = {
    email: '',
    password: ''
  };
  loginResponse = {} as LoginResponse;
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  onLogin() {
    // debugger;
    // this.authService.login(this.loginObj).subscribe(data => console.log(data));
    this.authService.login(this.loginObj).subscribe(data => {
      this.loginResponse = data
      console.log(this.loginResponse)
    });
  }
  // onLogin() {
  //   // debugger;
  //   this.http.post('https://localhost:7251/api/auth/login', this.loginObj).subscribe(
  //     (res: any) => {
  //       if (res.success != false) {
  //         console.log('Login OK');
  //       }
  //     },
  //     (error) => {
  //       if(error.status == 400){
  //         console.log('Erro na solicitação HTTP: ', error.error);
  //       } else {
  //         console.log('Erro desconhecido');
  //       }
  //     }
  //   );
  // }
}
