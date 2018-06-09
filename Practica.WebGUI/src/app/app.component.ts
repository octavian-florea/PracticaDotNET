import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: []
})
export class AppComponent {
  title = 'app';
  loggedIn: Boolean;

  constructor( private _authService:AuthService){
    this._authService.isLoggedIn.subscribe((value) => {
      this.loggedIn = value
    });
  }

  Logout(){
    this._authService.logout();
  }
}
