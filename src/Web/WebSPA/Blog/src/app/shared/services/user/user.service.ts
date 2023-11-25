import { Injectable } from '@angular/core';
import { BaseService } from '../base/base.service';
import { HttpClient } from '@angular/common/http';
import { AuthorRequest, AuthorResponse } from 'src/app/core/models/author/author.model';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService{

  private adminUrl = `${this.AdminUrl}`+ '/users';

  constructor(private httpClient: HttpClient) { super(); }

  getAuthUser(): Observable<AuthorResponse> {
    return this.httpClient.get<{ data: AuthorResponse }>(this.adminUrl, this.getAuthHeaderJson())
      .pipe(
        map((response: { data: AuthorResponse }) => response.data)
      );
  }

  putAuthUser(user: AuthorRequest) {
    return this.httpClient.put<void>(this.adminUrl, user, this.getAuthHeaderJson());
  }

}
