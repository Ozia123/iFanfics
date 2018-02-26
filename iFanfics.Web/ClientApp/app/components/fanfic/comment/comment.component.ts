import { Component, Input, OnInit } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { CommentModel } from '../../../models/CommentModel';
import { CommentRatingModel } from '../../../models/CommentRatingModel';
import { HttpAuthService } from '../../../services/http.auth.service';
import { HttpCommentService } from '../../../services/http.comment.service';
import { HttpCommentRatingService } from '../../../services/http.comment.rating.service';

@Component({
    selector: 'comment',
    templateUrl: './comment.component.html',
    styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
    public isOwner: boolean = false;
    public isAuthorized: boolean = false;
    public Given: CommentRatingModel = new CommentRatingModel();
    public commentRating: number = 0;
    public commentRatings: CommentRatingModel[] = [];

    constructor(
        private httpAuthService: HttpAuthService,
        private httpCommentService: HttpCommentService,
        private httpCommentRatingService: HttpCommentRatingService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, ) { }

    private checkOwnerStatus(): boolean {
        let role: string = localStorage.getItem("currentUserRole") || '';
        if (role == "Admin") {
            return true;
        }
        if (role == "User") {
            return (localStorage.getItem("currentUser") || '') == this.comment.username;
        }
        return false;
    }

    private async Initialize() {
        this.commentRatings = await this.httpCommentRatingService.getCommentRatings(this.comment.id);

        let currentUser: string = localStorage.getItem("currentUser") || '';
        if (currentUser != '') {
            this.isAuthorized = true;
        }
        for (let rating of this.commentRatings) {
            if (rating.username == currentUser) {
                this.Given = rating;
            }
            this.commentRating += rating.givenRating;
        }
    }

    ngOnInit() {
        this.isOwner = this.checkOwnerStatus();
        this.Initialize();
    }
    @Input() public comment: CommentModel;

    async onDelete() {
        const response = await this.httpCommentService.DeleteComment(this.comment.id);
        if (response.status == 200) {
            this.comment.comment = '';
            console.log('ok: comment deleted');
        }
        if (response.status == 400) {
            console.log('400: delete error');
        }
    }

    async onAddRating(givenRating: number) {
        if (this.Given.id != '') {
            if (this.Given.givenRating == givenRating) {
                return;
            }
            await this.httpCommentRatingService.deleteCommentRating(this.Given.id);
            this.Given = new CommentRatingModel();
            this.commentRating += givenRating;
            return;
        }
        this.Given.commentId = this.comment.id;
        this.Given.givenRating = givenRating;
        this.Given = (await this.httpCommentRatingService.createCommentRating(this.comment.id, this.Given)).json();
        this.commentRating += givenRating;
    }
}
