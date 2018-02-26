import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { HttpQueryStrings } from '../helpers/HttpQueryStrings';
import { CommentModel } from '../models/CommentModel';

@Injectable()
export class HttpCommentService {
    constructor(
        private http: Http,
        private router: Router,
        @Inject('BASE_URL') private baseUrl: string
    ) { }

    private options: any = {
        withCredentials: true
    };

    public async getFanficComments(id: string): Promise<CommentModel[]> {
        console.log('getting chapters');
        let comments: CommentModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetFanficComments + id, this.options).toPromise()).json();
        return comments;
    }

    public async createComment(id: string, comment: CommentModel) {
        let response;
        try {
            response = await this.http.post(this.baseUrl + HttpQueryStrings.CreateComment + id, comment, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }

    public async DeleteComment(id: string) {
        let response;
        try {
            response = await this.http.delete(this.baseUrl + HttpQueryStrings.DeleteComment + id, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }
}