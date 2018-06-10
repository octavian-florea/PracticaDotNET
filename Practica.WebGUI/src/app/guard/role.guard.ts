import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AuthService } from '../services/auth.service';

@Injectable()
export class RoleGuard implements CanActivate {

  constructor(public auth: AuthService, public router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
      const expectedRoles = next.data.expectedRoles;
      if(this.auth.hasUserRole(expectedRoles)){
        return true;
      }else{
        this.router.navigate(['/login']);
        return false;
      }

    
  }
}
