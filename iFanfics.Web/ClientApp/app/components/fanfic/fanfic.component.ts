import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { FanficModel } from '../../models/FanficModel';
import { ChapterModel } from '../../models/ChapterModel';
import { CommentModel } from '../../models/CommentModel';
import { HttpFanficService } from '../../services/http.fanfic.service';
import { HttpChapterService } from '../../services/http.chapter.service';
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

    public chapter: ChapterModel = new ChapterModel();
    public comment: CommentModel = new CommentModel();

    public chapterSelected: boolean = false;
    public isAuthenticated: boolean = false;
    public isValid: boolean = false;

    constructor(
        private httpChapterService: HttpChapterService,
        private httpFanficService: HttpFanficService,
        private httpCommentService: HttpCommentService,
        private router: Router,
        private route: ActivatedRoute,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, ) { }

    ngOnInit(): void {
        this.getFanfic();
        this.isAuthenticated = this.checkAuth();
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
        return (localStorage.getItem("currentUser") || '') != '';
    }

    public checkValidation() {
        this.isValid = this.comment.comment != '';
    }

    public selectChapter(chapter: ChapterModel) {
        if (this.chapter.id == chapter.id) {
            this.chapterSelected = false;
            this.chapter = new ChapterModel();
            return;
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
}
