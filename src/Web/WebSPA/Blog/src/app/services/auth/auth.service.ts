import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { Login, LoginResponse } from 'src/app/models/auth/login';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authUrl = `${environment.api}`+ '/auth';
  constructor(private httpClient: HttpClient) { }

  login(login: Login) : Observable<LoginResponse>{
    return this.httpClient.post<{ data: LoginResponse }>(`${this.authUrl}/login`, login)
      .pipe(
        map((response: { data: LoginResponse }) => response.data)
      );
  }
}
