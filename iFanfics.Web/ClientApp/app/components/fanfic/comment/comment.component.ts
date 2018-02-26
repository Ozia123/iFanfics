import { Component, Input, OnInit } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { CurrentUserModel } from '../../../models/CurrentUserModel';
import { Title } from '@angular/platform-browser';

import { CommentModel } from '../../../models/CommentModel';
import { HttpAuthService } from '../../../services/http.auth.service';
import { HttpCommentService } from '../../../services/http.comment.service';

@Component({
    selector: 'comment',
    templateUrl: './comment.component.html',
    styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
    public isOwner: boolean = false;

    constructor(
        private httpAuthService: HttpAuthService,
        private httpCommentService: HttpCommentService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private titleService: Title, ) {
    }

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

    ngOnInit() {
        this.isOwner = this.checkOwnerStatus();
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
}
