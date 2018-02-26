import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../../models/FanficModel';
import { HttpFanficService } from '../../../services/http.fanfic.service';
import { FanficShortComponent } from '../fanfic-short/fanfic-short.component';

@Component({
    selector: 'bytag',
    templateUrl: './fanfics-by-tag.component.html',
    styleUrls: ['./fanfics-by-tag.component.css'],
    providers: [HttpFanficService]
})
export class FanficsByTagComponent {
    public tagName: string = '';
    public fanfics: FanficModel[];

    constructor(
        public httpFanficService: HttpFanficService,
        private router: Router,
        private route: ActivatedRoute,
        private activatedRoute: ActivatedRoute,
        private titleService: Title) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.tagName = params['id'];
            this.Initialize();
        });
    }

    async Initialize() {
        this.fanfics = await this.httpFanficService.getAllByTag(this.tagName);
    }
}
