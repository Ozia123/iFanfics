import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../models/FanficModel';
import { HttpFanficService } from '../../services/http.fanfic.service';

@Component({
    selector: 'fanfic',
    templateUrl: './fanfic.component.html',
    styleUrls: ['./fanfic.component.css'],
    providers: [HttpFanficService]
})
export class FanficComponent {
    public fanfic: FanficModel;

    constructor(
        private httpFanficService: HttpFanficService,
        private router: Router,
        private route: ActivatedRoute,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, ) { }

    ngOnInit(): void {
        this.getFanfic();
    }

    async getFanfic() {
        const id: string = this.route.snapshot.paramMap.get('id') || '';
        if (id == '') {
            this.router.navigate(['/home']);
        }

        this.fanfic = await this.httpFanficService.getFanfic(id);
    }
}