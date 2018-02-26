import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';
import { Location } from '@angular/common';

import { FanficModel } from '../../../models/FanficModel';
import { ChapterModel } from '../../../models/ChapterModel';
import { TagModel } from '../../../models/TagModel';
import { GenreModel } from '../../../models/GenreModel';
import { HttpFanficService } from '../../../services/http.fanfic.service';
import { HttpChapterService } from '../../../services/http.chapter.service';

@Component({
    selector: 'edit/fanfic',
    templateUrl: './fanfic-edit.component.html',
    styleUrls: ['./fanfic-edit.component.css'],
    providers: [HttpFanficService, HttpChapterService]
})
export class FanficEditComponent {
    public fanfic: FanficModel = new FanficModel();
    public chapter: ChapterModel = new ChapterModel();

    public isValid: boolean = false;
    public isValidTag: boolean = false;
    public isChapterValid: boolean = false;
    public addChapter: boolean = false;
    public chapterEditIndex: number = -1;
    public serverErrors: string;

    public genres: string[] = [];
    public tags: string[] = [];
    public chapters: ChapterModel[] = [];
    public tag: string = '';

    constructor(
        private httpChapterService: HttpChapterService,
        private httpFanficService: HttpFanficService,
        private router: Router,
        private route: ActivatedRoute,
        private activatedRoute: ActivatedRoute,
        private titleService: Title,
        private location: Location)
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
        this.chapters = await this.httpChapterService.getFanficChapters(this.fanfic.id);
        this.tags = this.fanfic.tags;
    }

    private async Initialize() {
        let genres: GenreModel[] = await this.httpFanficService.getAllGenres();
        this.genres = [];
        for (let genre of genres) {
            this.genres.push(genre.genreName);
        }
    }

    public checkValidation() {
        this.isValid = this.fanfic.title != ''
            && this.fanfic.description != ''
            && this.fanfic.title.length < 20
            && this.fanfic.description.length < 300;
    }

    public checkChapterValidation() {
        this.isChapterValid = this.chapter.title != ''
            && this.chapter.title.length < 20
            && this.chapter.chapterText != ''
            && this.chapter.chapterText.length < 3000;
    }

    public onTagInput(tag: string) {
        this.tag = tag;
        this.isValidTag = this.tag != '' && this.tag.length < 15;
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

    public onAddChapter() {
        this.addChapter = true;
        this.chapter = new ChapterModel();
    }

    public onAddChapterSubmit() {
        this.onAddChapterSubmitAsync();
    }

    public async onAddChapterSubmitAsync() {
        if (this.chapterEditIndex > -1) {
            this.chapter = (await this.httpChapterService.editChapter(this.fanfic.id, this.chapter)).json();
            this.chapters[this.chapterEditIndex] = this.chapter;
            this.chapterEditIndex = -1;
        }
        else {
            this.chapter = (await this.httpChapterService.createChapter(this.fanfic.id, this.chapter)).json();
            this.chapters.push(this.chapter);
        }
        this.addChapter = false;
    }

    public onChapterEdit(chapter: ChapterModel) {
        let index = this.chapters.indexOf(chapter, 0);
        this.chapter = this.chapters[index];
        this.chapterEditIndex = index;
        this.addChapter = true;
    }

    public async onChapterDelete(chapter: ChapterModel) {
        let index = this.chapters.indexOf(chapter, 0);
        if (index > -1) {
            await this.httpChapterService.DeleteChapter(this.chapters[index].id);
            this.chapters.splice(index, 1);
        }
    }

    public async onTagSubmit() {
        for (let tag of this.tags) {
            if (tag == this.tag) {
                return;
            }
        }
        let tag: TagModel = new TagModel();
        tag.tagName = this.tag;
        const response = await this.httpFanficService.AddTag(this.fanfic.id, tag);
        this.tags.push(this.tag);
    }

    public async onTagDelete(tag: string) {
        let index = this.tags.indexOf(tag, 0);
        if (index > -1) {
            let tag: TagModel = new TagModel();
            tag.tagName = this.tags[index];
            const response = await this.httpFanficService.DeleteTag(this.fanfic.id, tag);
            this.tags.splice(index, 1);
        }
    }

    public goBack() {
        this.location.back();
    }
}