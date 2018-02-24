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
    selector: 'create',
    templateUrl: './fanfic-create.component.html',
    styleUrls: ['./fanfic-create.component.css'],
    providers: [HttpFanficService, HttpFanficService]
})
export class FanficCreateComponent {
    currentUser: CurrentUserModel = new CurrentUserModel();
    public newFanfic: CreateFanficModel = new CreateFanficModel();

    public isValid: boolean = false;
    public isValidTag: boolean = false;
    public serverErrors: string;

    public genres: string[];
    public tags: string[] = [];
    public tag: string = '';

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
        let genres: GenreModel[] = await this.httpFanficService.getAllGenres();
        this.genres = [];
        for (let genre of genres) {
            this.genres.push(genre.genreName);
        }
    }

    public async AddTag(tagName: string) {
        let tag: TagModel = new TagModel();
        tag.tagName = tagName;
        await this.httpFanficService.CreateTag(tag);
    }
    
    private checkValidation() {
        this.isValid = this.newFanfic.title != ''
            && this.newFanfic.description != ''
            && this.newFanfic.genre != '';
    }

    public onTitleInput(title: string) {
        this.newFanfic.title = title;
        this.checkValidation();
    }

    public onDescriptionInput(description: string) {
        this.newFanfic.description = description;
        this.checkValidation();
    }

    public onTagInput(tag: string) {
        this.tag = tag;
        this.isValidTag = this.tag != '';
    }

    public async onSubmit() {
        this.newFanfic.pictureUrl = 'https://firebasestorage.googleapis.com/v0/b/ifanfics-3917e.appspot.com/o/default-fanfic.jpg?alt=media&token=5848e00e-b8a0-4c7e-b0b3-d06a199afac2';
        this.newFanfic.tags = this.tags;
        const response = await this.httpFanficService.CreateFanfic(this.newFanfic);

        if (response.status == 200) {
            console.log('ok: fanfic created');
            this.router.navigate(['/home']);
        }
        if (response.status == 400) {
            this.serverErrors = 'title already used, think about another one';
        }
    }

    public onTagSubmit() {
        for (let tag of this.tags) {
            if (tag == this.tag) {
                return;
            }
        }
        this.tags.push(this.tag);
    }

    onTagDelete(tag: string) {
        let index = this.tags.indexOf(tag, 0);
        if (index > -1) {
            this.tags.splice(index, 1);
        }
    }
}
