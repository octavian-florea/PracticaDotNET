import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import * as decode from 'jwt-decode';
import { User } from '../models/user.model';
import 'rxjs/add/operator/take';

@Injectable()
export class AuthService{

    readonly rootURL = "http://localhost:64196/";
    private loggedIn = new BehaviorSubject<Boolean>(this.tokenExistsAndNotExpired());
    private userData = new BehaviorSubject<User>(this.getInitialUserFromToken());

    get isLoggedIn() {
        return this.loggedIn.asObservable();
    }

    get getUserData() {
        return this.userData.asObservable();
    }

    constructor(private _http: HttpClient, private router: Router){

    }

    getEmailExistsHttp(email:string){
        return this._http.get(this.rootURL+'api/auth/emailexists/'+email).take(1);
    }

    registerHttp(user: object): void{
        this._http.post(this.rootURL+'api/auth/register', user).take(1).subscribe(
            (res:any) => { 
                this.login(res);
            },
            (err) => { }
          );
    }

    loginHttp(user: object): void{
        this._http.post(this.rootURL+'api/auth/login', user).take(1).subscribe(
            (res:any) => { 
                this.login(res);
            },
            (err) => { }
          ); 
    }

    logout(): void{
        localStorage.removeItem('userToken');
        this.loggedIn.next(false);
        this.userData.next({Email:'',Password:'',Role:''});
        this.router.navigate(['']);
    }

    hasUserRole(expectedRoles): boolean{
        let tokenRole = this.getRoleFromToken();
        for (let role of expectedRoles) {
            if (tokenRole == role) {
                return true;
            }
        }
        return false; 
    }

    private login(res:any): void{
        localStorage.setItem('userToken',res.token);
        this.loggedIn.next(true);
        this.userData.next(this.getUserFromToken(res.token)); 
        this.router.navigate(['']);
    }
    
    private getUserFromToken(token): User{
        let user: User;
        user = {
            Email:this.getDecodedAccessToken(token).sub,
            Password:'',
            Role: this.getRoleFromToken()
        }
        return user;
    }

    private tokenExistsAndNotExpired(): boolean {
        var flagInitialIsLoggedIn = false;
        var userToken = localStorage.getItem('userToken');
        if(userToken!=null){
            flagInitialIsLoggedIn = true;
            const tokenPayload = this.getDecodedAccessToken(userToken);
            var current_time = new Date().getTime() / 1000;
            if (current_time > tokenPayload.exp){
                flagInitialIsLoggedIn = false;
            }
        }
        return flagInitialIsLoggedIn;
    }

    private getInitialUserFromToken(): User  {
        let user = {Email:'',Password:'',Role:''};
        var userToken = localStorage.getItem('userToken');
        if(userToken!=null){
            user = this.getUserFromToken(userToken);
        }
        return user;
    }

    private getDecodedAccessToken(token: string): any {
        try{
            return decode(token);
        }
        catch(Error){
            return null;
        }
    }

    private getRoleFromToken(): string{
        var role = '';
        const token = localStorage.getItem('userToken');
        const tokenPayload = this.getDecodedAccessToken(token);
        Object.keys(tokenPayload).forEach(function(key) {
            if(key.endsWith('/role')){
                role = tokenPayload[key];
            }
        });
        return role;
    }
}