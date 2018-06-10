import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { User } from './models/user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: []
})
export class AppComponent {
  title = 'app';
  loggedIn: Boolean;
  user: User;

  constructor( private _authService:AuthService){
    this._authService.isLoggedIn.subscribe((value) => {
      this.loggedIn = value
    });
    this._authService.getUserData.subscribe((value) => {
      this.user = value
    });
  }

  Logout(){
    this._authService.logout();
  }
}
