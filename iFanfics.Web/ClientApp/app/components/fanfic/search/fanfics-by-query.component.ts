import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../../models/FanficModel';
import { HttpFanficService } from '../../../services/http.fanfic.service';
import { FanficShortComponent } from '../fanfic-short/fanfic-short.component';

@Component({
    selector: 'byquery',
    templateUrl: './fanfics-by-query.component.html',
    styleUrls: ['./fanfics-by-query.component.css'],
    providers: [HttpFanficService]
})
export class FanficsByQueryComponent {
    public query: string = '';
    public fanfics: FanficModel[];

    constructor(
        public httpFanficService: HttpFanficService,
        private router: Router,
        private route: ActivatedRoute,
        private activatedRoute: ActivatedRoute,
        private titleService: Title) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.query = params['id'];
            this.Initialize();
        });
    }

    async Initialize() {
        this.fanfics = await this.httpFanficService.getAllByQuery(this.query);
    }
}
