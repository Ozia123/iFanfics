import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";

import { HttpAuthService } from "../services/http.auth.service"

@Injectable()
export class AdminGuard implements CanActivate {
    constructor(private httpAuthService: HttpAuthService, private router: Router) { }

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.httpAuthService.currentUser == null) {
            this.router.navigate(['/home']);
            return false;
        }
        if (!this.httpAuthService.currentUser.isAuntificated) {
            this.router.navigate(['/home']);
        }
        else {
            return true;
        }
        return false;
    }
}