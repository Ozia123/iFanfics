import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';


import { CreateFanficModel } from '../../../../models/CreateFanficModel';
import { HttpAuthService } from '../../../../services/http.auth.service';
import { HttpFanficService } from '../../../../services/http.fanfic.service';

@Component({
    selector: 'createchapter',
    templateUrl: './fanfic-create.component.html',
    providers: [HttpFanficService, HttpFanficService]
})
export class ChapterCreateComponent {
    constructor(
        private httpAuthService: HttpAuthService,
        private httpFanficService: HttpFanficService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, ) {
    }


    ngOnInit(): void {
        this.getFanfic();
    }

    private getFanfic() {

    }
}