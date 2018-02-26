import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { HttpQueryStrings } from '../helpers/HttpQueryStrings';
import { CommentRatingModel } from '../models/CommentRatingModel';

@Injectable()
export class HttpCommentRatingService {
    constructor(
        private http: Http,
        private router: Router,
        @Inject('BASE_URL') private baseUrl: string
    ) { }

    private options: any = {
        withCredentials: true
    };

    public async getCommentRatings(id: string): Promise<CommentRatingModel[]> {
        let ratings: CommentRatingModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetCommentRatings + id, this.options).toPromise()).json();
        return ratings;
    }

    public async createCommentRating(id: string, rating: CommentRatingModel) {
        let response;
        try {
            response = await this.http.post(this.baseUrl + HttpQueryStrings.CreateCommentRating + id, rating, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }

    public async deleteCommentRating(id: string) {
        let response;
        try {
            response = await this.http.delete(this.baseUrl + HttpQueryStrings.DeleteCommentRating + id, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }
}