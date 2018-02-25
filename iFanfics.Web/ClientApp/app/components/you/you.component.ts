import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../models/FanficModel';
import { HttpAuthService } from '../../services/http.auth.service';
import { HttpFanficService } from '../../services/http.fanfic.service';
import { FanficShortComponent } from '../fanfic/fanfic-short/fanfic-short.component';

@Component({
    selector: 'you',
    templateUrl: './you.component.html',
    styleUrls: ['./you.component.css'],
    providers: [HttpFanficService]
})
export class YouComponent {
    currentUser: CurrentUserModel = new CurrentUserModel();
    public fanfics: FanficModel[];

    constructor(
        private httpAuthService: HttpAuthService,
        private httpFanficService: HttpFanficService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, )
    {
        this.currentUser = httpAuthService.currentUser;
        this.Initialize();
    }

    async Initialize() {
        this.fanfics = await this.httpFanficService.getAllUsers(this.currentUser.userName);
    } 
}
