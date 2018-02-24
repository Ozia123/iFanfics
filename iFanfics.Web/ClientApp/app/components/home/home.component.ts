import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../models/FanficModel';
import { HttpFanficService } from '../../services/http.fanfic.service';

//https://firebasestorage.googleapis.com/v0/b/ifanfics-3917e.appspot.com/o/default-fanfic.jpg?alt=media&token=5848e00e-b8a0-4c7e-b0b3-d06a199afac2

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    providers: [HttpFanficService]
})
export class HomeComponent {
    public fanfics: FanficModel[];

    constructor(
        public httpFanficService: HttpFanficService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, )
    {
        this.Initialize();
    }

    async Initialize() {
        this.fanfics = await this.httpFanficService.getAll();
    }
}
