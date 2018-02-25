import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../models/FanficModel';
import { HttpAuthService } from '../../services/http.auth.service';
import { HttpFanficService } from '../../services/http.fanfic.service';

@Component({
    selector: 'you',
    templateUrl: './you.component.html',
    styleUrls: ['./you.component.css'],
    providers: [HttpFanficService, HttpFanficService]
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

    onEdit(fanfic: FanficModel) {
        localStorage.setItem('resource', fanfic.id);
        this.router.navigate(['/edit/fanfic/' + fanfic.id]);
    }

    async onDelete(fanfic: FanficModel) {
        let index = this.fanfics.indexOf(fanfic, 0);
        if (index > -1) {
            this.fanfics.splice(index, 1);
        }

        const response = await this.httpFanficService.DeleteFanfic(fanfic.id);
        if (response.status == 200) {
            console.log('ok: fanfic deleted');
        }
        if (response.status == 400) {
            console.log('400: delete error');
        }
    }
}
