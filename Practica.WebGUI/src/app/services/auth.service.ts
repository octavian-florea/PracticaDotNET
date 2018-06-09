import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { BehaviorSubject } from 'rxjs';
import { MatDialog } from '@angular/material';
import { Router } from '@angular/router';
import { ErrorDialogComponent } from '../dialog/errorDialog.component';

@Injectable()
export class AuthService{

    readonly rootURL = "http://localhost:64196/";
    private loggedIn = new BehaviorSubject<Boolean>(false);

    get isLoggedIn() {
        return this.loggedIn.asObservable();
    }

    constructor(private _http: HttpClient, public dialog : MatDialog, private router: Router){

    }

    getUserDataHttp(){
        console.log('get user data - http');

        return this._http.get(this.rootURL+'api/activity');
    }

    getEmailExistsHttp(email:string){
        return this._http.get(this.rootURL+'api/auth/emailexists/'+email);
    }

    registerHttp(user: object){
        this._http.post(this.rootURL+'api/auth/register', user).subscribe(
            (res:any) => { 
              localStorage.setItem('userToken',res.token);
              this.loggedIn.next(true);
              this.router.navigate(['']);
            },
            (err) => { this.showError(err.error) }
          );
    }

    loginHttp(user: object){
        return this._http.post(this.rootURL+'api/auth/login', user)
            .map(data => {
                console.log(data);
                // login successful if there's a jwt token in the response
                //if (user && user.token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                //    localStorage.setItem('currentUser', JSON.stringify(user));
                //}

                return user;
            });     
    }

    logout(){
        localStorage.removeItem('userToken');
        this.loggedIn.next(false);
        this.router.navigate(['']);
    }

    showError(error : string) : void {
        this.dialog.open(ErrorDialogComponent, {
          data: {errorMsg: error} ,width : '250px'
        });
      }
}