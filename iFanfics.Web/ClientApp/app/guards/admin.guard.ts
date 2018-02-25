import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";

import { HttpAuthService } from "../services/http.auth.service"

@Injectable()
export class AdminGuard implements CanActivate {
    constructor(private httpAuthService: HttpAuthService, private router: Router) { }

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (!this.httpAuthService.currentUser.isAuntificated) {
            this.router.navigate(['/home']);
        }
        else {
            let index = this.httpAuthService.currentUser.roles.indexOf('Admin', 0);
            if (index > -1) {
                return true;
            }
            this.router.navigate(['/home']);
        }
        return false;
    }
}