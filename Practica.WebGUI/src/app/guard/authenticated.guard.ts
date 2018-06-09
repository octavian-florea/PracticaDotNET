import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AuthService } from '../services/auth.service';
import { take } from 'rxjs/operators';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthenticatedGuard implements CanActivate {

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
          if (!isLoggedIn){
            this.router.navigate(['/login']);  
            return false;
          }
          return true;
        })
      )
  }
}
