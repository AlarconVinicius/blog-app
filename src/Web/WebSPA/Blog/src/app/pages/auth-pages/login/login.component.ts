import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Login } from 'src/app/models/auth/login';

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
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  onLogin() {
    // debugger;
    this.http.post('https://localhost:7251/api/auth/login', this.loginObj).subscribe(
      (res: any) => {
        if (res.success != false) {
          console.log('Login OK');
        }
      },
      (error) => {
        if(error.status == 400){
          console.log('Erro na solicitação HTTP: ', error.error);
        } else {
          console.log('Erro desconhecido');
        }
      }
    );
  }
}
