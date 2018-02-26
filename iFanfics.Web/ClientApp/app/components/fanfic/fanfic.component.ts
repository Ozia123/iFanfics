import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../models/FanficModel';
import { ChapterModel } from '../../models/ChapterModel';
import { ChapterRatingModel } from '../../models/ChapterRatingModel';
import { CommentModel } from '../../models/CommentModel';
import { HttpFanficService } from '../../services/http.fanfic.service';
import { HttpChapterService } from '../../services/http.chapter.service';
import { HttpChapterRatingService } from '../../services/http.chapter.rating.service';
import { HttpCommentService } from '../../services/http.comment.service';

import { CommentComponent } from './comment/comment.component';

@Component({
    selector: 'fanfic',
    templateUrl: './fanfic.component.html',
    styleUrls: ['./fanfic.component.css'],
    providers: [HttpFanficService, HttpChapterService, HttpCommentService]
})
export class FanficComponent {
    public fanfic: FanficModel;
    public chapters: ChapterModel[] = [];
    public comments: CommentModel[] = [];
    public currentUser: string = '';
    public isAuthorized: boolean = false;

    public Given: ChapterRatingModel = new ChapterRatingModel();
    public chapterRating: number = 0;
    public chapterRatings: ChapterRatingModel[] = [];

    public chapter: ChapterModel = new ChapterModel();
    public comment: CommentModel = new CommentModel();

    public chapterSelected: boolean = false;
    public isValid: boolean = false;

    constructor(
        private httpChapterService: HttpChapterService,
        private httpFanficService: HttpFanficService,
        private httpCommentService: HttpCommentService,
        private httpChapterRatingService: HttpChapterRatingService,
        private router: Router,
        private route: ActivatedRoute,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, ) { }

    ngOnInit(): void {
        this.getFanfic();
        this.isAuthorized = this.checkAuth();
    }

    async getFanfic() {
        const id: string = this.route.snapshot.paramMap.get('id') || '';
        if (id == '') {
            this.router.navigate(['/home']);
        }

        this.fanfic = await this.httpFanficService.getFanfic(id);
        this.chapters = await this.httpChapterService.getFanficChapters(this.fanfic.id);
        this.comments = await this.httpCommentService.getFanficComments(this.fanfic.id);
    }

    private checkAuth(): boolean {
        this.currentUser = localStorage.getItem("currentUser") || '';
        return this.currentUser != '';
    }

    public checkValidation() {
        this.isValid = this.comment.comment != '' && this.comment.comment.length < 50;
    }

    public async selectChapter(chapter: ChapterModel) {
        if (this.chapter.id == chapter.id) {
            this.chapterSelected = false;
            this.chapter = new ChapterModel();
            this.Given = new ChapterRatingModel();
            this.chapterRating = 0;
            return;
        }

        this.chapterRatings = await this.httpChapterRatingService.getChapterRatings(chapter.id);

        for (let rating of this.chapterRatings) {
            if (rating.username == this.currentUser) {
                this.Given = rating;
            }
            this.chapterRating += rating.givenRating;
        }

        this.chapterSelected = true;
        this.chapter = chapter;
    }

    public async onCommentSubmit() {
        const response = await this.httpCommentService.createComment(this.fanfic.id, this.comment);

        if (response.status == 200) {
            this.comment = new CommentModel();
            this.isValid = false;
            this.comments.push(response.json());
        }
    }

    public async onAddRating(givenRating: number) {
        if (this.Given.id != '') {
            if (this.Given.givenRating == givenRating) {
                return;
            }
            await this.httpChapterRatingService.deleteChapterRating(this.Given.id);
            this.Given = new ChapterRatingModel();
            this.chapterRating += givenRating;
            return;
        }
        this.Given.chapterId = this.chapter.id;
        this.Given.givenRating = givenRating;
        this.Given = (await this.httpChapterRatingService.createChapterRating(this.chapter.id, this.Given)).json();
        this.chapterRating += givenRating;
    }
}
