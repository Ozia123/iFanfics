import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { CreateFanficModel } from '../../../models/CreateFanficModel';
import { TagModel } from '../../../models/TagModel';
import { GenreModel } from '../../../models/GenreModel';
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

    public isValid: boolean;
    public tags: string[];
    private Title: string;
    private Description: string;

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

    private async Initialize() {
        
    }
    
    private checkValidation() {
        this.isValid = this.newFanfic.genre != '' && this.newFanfic.title != '' && this.newFanfic.description != '';
    }

    public onTitleInput(title: string) {
        this.newFanfic.title = title;
        this.checkValidation();
    }

    public onDescriptionInput(description: string) {
        this.newFanfic.description = description;
        this.checkValidation();
    }


}
