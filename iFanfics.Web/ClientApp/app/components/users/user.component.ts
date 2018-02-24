import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../models/FanficModel';
import { HttpUserService } from '../../services/http.user.service';
import { HttpFanficService } from '../../services/http.fanfic.service';

@Component({
    selector: 'user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css'],
    providers: [HttpFanficService, HttpUserService]
})
export class UserComponent {
    public currentUser: CurrentUserModel = new CurrentUserModel();
    public fanfics: FanficModel[];

    constructor(
        private httpUserService: HttpUserService,
        private httpFanficService: HttpFanficService,
        private router: Router,
        private route: ActivatedRoute,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, ) { }

    ngOnInit(): void {
        this.getUser();
    }

    async getUser() {
        const id: string = this.route.snapshot.paramMap.get('id') || '';
        if (id == '') {
            this.router.navigate(['/home']);
        }

        this.currentUser = await this.httpUserService.getUserProfile(id);
        if (this.currentUser == null) {
            this.router.navigate(['/home']);
        }

        this.fanfics = await this.httpFanficService.getAllUsers(id);
    }
}
