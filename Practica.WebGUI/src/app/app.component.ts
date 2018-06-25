import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { User } from './models/user.model';
import { ActivityService } from './services/activity.service';
import { Search } from './models/search.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: []
})
export class AppComponent {
  title = 'Practica';
  loggedIn: Boolean;
  user: User;
  searchValue = '';

  constructor( private _authService:AuthService, private _activityService: ActivityService){
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

  search(value){
    let searchObj: Search = {
      SearchKey: value,
      City:""
    }
    this._activityService.setSearch(searchObj);
  }

  refreshSearch(){
    this.search("");
  }
}
