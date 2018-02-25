import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../models/FanficModel';
import { HttpFanficService } from '../../services/http.fanfic.service';
import { FanficShortComponent } from '../fanfic/fanfic-short/fanfic-short.component';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    providers: [HttpFanficService]
})
export class HomeComponent {
    public pageNumber: number = 1;
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
