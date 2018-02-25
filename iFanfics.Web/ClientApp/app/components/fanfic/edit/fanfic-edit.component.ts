import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../../models/FanficModel';
import { ChapterModel } from '../../../models/ChapterModel';
import { TagModel } from '../../../models/TagModel';
import { GenreModel } from '../../../models/GenreModel';
import { HttpFanficService } from '../../../services/http.fanfic.service';

@Component({
    selector: 'edit/fanfic',
    templateUrl: './fanfic-edit.component.html',
    styleUrls: ['./fanfic-edit.component.css'],
    providers: [HttpFanficService]
})
export class FanficEditComponent {
    public fanfic: FanficModel = new FanficModel();

    public isValid: boolean = false;
    public isValidTag: boolean = false;
    public serverErrors: string;

    public genres: string[] = [];
    public tags: string[] = [];
    public tag: string = '';

    constructor(
        private httpFanficService: HttpFanficService,
        private router: Router,
        private route: ActivatedRoute,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, )
    {
        this.Initialize();
    }

    ngOnInit(): void {
        this.getFanfic();
    }

    async getFanfic() {
        const id: string = this.route.snapshot.paramMap.get('id') || '';
        if (id == '') {
            this.router.navigate(['/home']);
        }

        this.fanfic = await this.httpFanficService.getFanfic(id);
        this.tags = this.fanfic.tags;
    }

    private async Initialize() {
        let genres: GenreModel[] = await this.httpFanficService.getAllGenres();
        this.genres = [];
        for (let genre of genres) {
            this.genres.push(genre.genreName);
        }
    }

    private checkValidation() {
        this.isValid = this.fanfic.title != ''
            && this.fanfic.description != ''
    }

    public onTagInput(tag: string) {
        this.tag = tag;
        this.isValidTag = this.tag != '';
    }

    public async onSubmit() {
        this.fanfic.tags = this.tags;
        const response = await this.httpFanficService.EditFanfic(this.fanfic);

        if (response.status == 200) {
            this.router.navigate(['/you']);
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