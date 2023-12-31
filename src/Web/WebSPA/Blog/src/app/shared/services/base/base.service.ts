import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService {

  public localStorage = new LocalStorageUtils();
  protected BlogApi: string = environment.blogApi;
  
  protected getHeaderJson(){
      return {
          headers: new HttpHeaders({
              'Content-Type': 'application/json',
          })
      }
  }

  protected getAuthHeaderJson() {
      return {
          headers: new HttpHeaders({
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${this.localStorage.getUserToken()}`
          })
      };
  }
}
