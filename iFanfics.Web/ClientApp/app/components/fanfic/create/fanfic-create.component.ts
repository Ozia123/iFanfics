import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { CreateFanficModel } from '../../../models/CreateFanficModel';
import { HttpAuthService } from '../../../services/http.auth.service';
import { HttpFanficService } from '../../../services/http.fanfic.service';

@Component({
    selector: 'fanfic-create',
    templateUrl: './fanfic-create.component.html',
    styleUrls: ['./fanfic-create.component.css'],
    providers: [HttpFanficService, HttpFanficService]
})
export class FanficCreateComponent {
    currentUser: CurrentUserModel = new CurrentUserModel();
    public newFanfic: CreateFanficModel;

    constructor(
        private httpAuthService: HttpAuthService,
        private httpFanficService: HttpFanficService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, )
    {
        this.currentUser = httpAuthService.currentUser;
    }
}
