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
        private titleService: Title, )
    {
        this.checkCurrentAuthUser();
    }

    public async logout() {
        await this.httpAuthService.Logout();
        this.checkCurrentAuthUser();
        localStorage.setItem('currentUser', this.httpAuthService.currentUser.userName);
        localStorage.setItem('currentUserRole', 'NotAuthorized');
        this.router.navigate(['']);
    }

    private async checkCurrentAuthUser() {
        await this.httpAuthService.getCurrentUser();
        this.currentUser = this.httpAuthService.currentUser;
        localStorage.setItem('currentUser', this.httpAuthService.currentUser.userName);
        if (this.httpAuthService.currentUser.roles.length == 0) {
            localStorage.setItem('currentUserRole', 'NotAuthorized');
        }
        else {
            let index = this.httpAuthService.currentUser.roles.indexOf('Admin', 0);
            if (index > -1) {
                localStorage.setItem('currentUserRole', 'Admin');
            }
            else {
                localStorage.setItem('currentUserRole', 'User');
            }
        }
    }

    public checkAuthStatus(): boolean {
        let user: string = localStorage.getItem('currentUser') || '';
        if (user == '') {
            return false
        }
        this.currentUser = this.httpAuthService.currentUser;
        if (!this.currentUser.isAuntificated) {
            this.checkCurrentAuthUser();
        }
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