import { Injectable } from '@angular/core';
import { Router, RouterStateSnapshot, ActivatedRouteSnapshot, CanActivate } from '@angular/router';
import { HttpAuthService } from '../services/http.auth.service';

@Injectable()
export class LoginGuard implements CanActivate {
    constructor(private httpAuthService: HttpAuthService, private router: Router) { }

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        await this.httpAuthService.getCurrentUser();
        if (this.httpAuthService.currentUser.isAuntificated) {
            this.router.navigate(['/home']);
        }
        else {
            return true;
        }
        return false;
    }
}