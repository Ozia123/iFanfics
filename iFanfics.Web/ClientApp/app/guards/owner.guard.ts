import { Injectable } from '@angular/core';
import { Router, RouterStateSnapshot, ActivatedRouteSnapshot, CanActivate, NavigationEnd, ActivatedRoute } from '@angular/router';
import { HttpAuthService } from '../services/http.auth.service';
import { HttpFanficService } from '../services/http.fanfic.service';
import { FanficModel } from '../models/FanficModel';


@Injectable()
export class OwnerGuard implements CanActivate {
    private id: string = '';

    constructor(
        private httpAuthService: HttpAuthService,
        private httpFanficService: HttpFanficService,
        private router: Router,
        private route: ActivatedRoute,
        private activatedRoute: ActivatedRoute) { }

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        this.id = localStorage.getItem('resource') || '';
        localStorage.setItem('resource', '');
        await this.httpAuthService.getCurrentUser();
        if (!this.httpAuthService.currentUser.isAuntificated || this.id == '') {
            this.router.navigate(['/home']);
        }
        else {
            let fanfic: FanficModel = await this.httpFanficService.getFanfic(this.id);
            let index = this.httpAuthService.currentUser.roles.indexOf('Admin', 0);
            if (fanfic.author_username == this.httpAuthService.currentUser.userName || index > -1) {
                return true;
            }
            this.router.navigate(['/home']);
        }
        return false;
    }
}