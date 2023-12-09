import { Component ,ElementRef} from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageUtils } from './shared/helpers/localstorage/localstorage';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'admindashboard';
  constructor(private elementRef: ElementRef,  public  _router: Router, private localStorage: LocalStorageUtils) { }

  ngOnInit() {

    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = "../assets/admin-template/js/main.js";
    this.elementRef.nativeElement.appendChild(s);
    this.checkTokenExpiration();
  }
  checkTokenExpiration(){
    if (this.localStorage.isLoggedIn() && this.localStorage.isTokenExpired()) {
      this.localStorage.clearLocalUserData();
      this._router.navigate(['']);
    }
  }
}
