import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

@Injectable()
export class AuthService{

    constructor(private _http: HttpClient){

    }

    getUserDataHttp(){
        console.log('get user data - http');

        return this._http.get('http://localhost:64196/api/activity');
    }

    registerHttp(user: object){
        console.log('register-http');

        return this._http.post('http://localhost:64196/api/auth/register', user);
    }
    loginHttp(user: object){
        console.log('login-http');

        return this._http.post('http://localhost:64196/api/auth/login', user)
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
}