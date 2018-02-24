import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../models/CurrentUserModel';
import { HttpAuthService } from '../../services/http.auth.service';
import { Title } from '@angular/platform-browser';

import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/mergeMap';

@Component({
    selector: 'auth',
    templateUrl: './auth.component.html',
    styleUrls: ['./auth.component.css'],
    providers: [HttpAuthService]
})

export class AuthComponent {
    currentUser: CurrentUserModel = new CurrentUserModel();

    constructor(
        public httpAuthService: HttpAuthService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, ) { }

    public async logout() {
        await this.httpAuthService.Logout();
        this.checkCurrentAuthUser();
        this.router.navigate(['']);
    }

    private async checkCurrentAuthUser() {
        await this.httpAuthService.getCurrentUser();
        this.currentUser = this.httpAuthService.currentUser;
    }

    public checkAuthStatus(): boolean {
        this.currentUser = this.httpAuthService.currentUser;
        return this.currentUser.isAuntificated;
    }

    async ngOnInit() {
        await this.checkCurrentAuthUser();

        this.router.events
            .filter(event => event instanceof NavigationEnd)
            .map(() => this.activatedRoute)
            .map(route => {
                while (route.firstChild) route = route.firstChild;
                return route;
            })
            .filter(route => route.outlet === 'primary')
            .mergeMap(route => route.data)
            .subscribe(async (event) => {

                this.titleService.setTitle(event['title'])
            });
    }
}