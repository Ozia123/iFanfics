import { Component, Input, OnInit } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../../models/FanficModel';
import { HttpAuthService } from '../../../services/http.auth.service';
import { HttpFanficService } from '../../../services/http.fanfic.service';

@Component({
    selector: 'fanfic-short',
    templateUrl: './fanfic-short.component.html',
    styleUrls: ['./fanfic-short.component.css']
})
export class FanficShortComponent implements OnInit {
    public isOwner: boolean = false;

    constructor(
        private httpFanficService: HttpFanficService,
        private httpAuthService: HttpAuthService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, )
    {
    }

    private checkOwnerStatus(): boolean {
        let role: string = localStorage.getItem("currentUserRole") || '';
        if (role == "Admin") {
            return true;
        }
        if (role == "User") {
            return (localStorage.getItem("currentUser") || '') == this.fanfic.author_username;
        }
        return false;
    }

    ngOnInit() {
        this.isOwner = this.checkOwnerStatus();
    }
    @Input() public fanfic: FanficModel;

    onEdit() {
        localStorage.setItem('resource', this.fanfic.id);
        this.router.navigate(['/edit/fanfic/' + this.fanfic.id]);
    }

    async onDelete() {
        const response = await this.httpFanficService.DeleteFanfic(this.fanfic.id);
        if (response.status == 200) {
            this.fanfic.title = '';
            console.log('ok: fanfic deleted');
        }
        if (response.status == 400) {
            console.log('400: delete error');
        }
    }
}
