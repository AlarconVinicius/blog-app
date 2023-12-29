import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { LoginRequest, LoginResponse } from 'src/app/core/models/auth/login.model';
import { BaseService } from '../base/base.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  private authUrl = `${this.BlogApi}`+ '/auth';
  constructor(private httpClient: HttpClient) { super ();}

  login(login: LoginRequest) : Observable<LoginResponse>{
    return this.httpClient.post<{ data: LoginResponse }>(`${this.authUrl}/login`, login, this.getHeaderJson())
      .pipe(
        map((response: { data: LoginResponse }) => response.data)
      );
  }
}
