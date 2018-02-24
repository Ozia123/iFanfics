import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";

import { HttpAuthService } from "../services/http.auth.service"

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private httpAuthService: HttpAuthService, private router: Router) { }

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        await this.httpAuthService.getCurrentUser();
        console.log('can activate: ' + this.httpAuthService.currentUser.isAuntificated);
        if (this.httpAuthService.currentUser.isAuntificated === false) {
            this.router.navigate(['/home']);
        }
        else {
            return true;
        }
        return false;
    }
}