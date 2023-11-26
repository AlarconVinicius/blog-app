import { Component, OnInit } from '@angular/core';
import { LocalStorageUtils } from 'src/app/shared/helpers/localstorage/localstorage';

@Component({
  selector: 'app-site-header',
  templateUrl: './site-header.component.html',
  styleUrls: ['./site-header.component.css']
})
export class SiteHeaderComponent implements OnInit {

  public LocalStorage = new LocalStorageUtils();
  loggedIn: boolean = false;
  constructor() { }

  ngOnInit() {
    this.isLoggedIn();
  }
  isLoggedIn(){
    if(this.LocalStorage.isLoggedIn()){
      this.loggedIn= true;
    } else {
      this.loggedIn= false;
    }
  }
}
