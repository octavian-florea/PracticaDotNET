import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AuthService } from '../services/auth.service';
import { take, map } from 'rxjs/operators';

@Injectable()
export class NotAuthenticatedGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
      return this.authService.isLoggedIn         
      .pipe(
        take(1),                              
        map((isLoggedIn: boolean) => {         
          if (isLoggedIn){
            console.log(isLoggedIn)
            this.router.navigate(['']);  
            return false;
          }
          return true;
        })
      )
  }
}
