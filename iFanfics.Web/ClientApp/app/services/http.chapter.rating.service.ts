import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { HttpQueryStrings } from '../helpers/HttpQueryStrings';
import { ChapterRatingModel } from '../models/ChapterRatingModel';

@Injectable()
export class HttpChapterRatingService {
    constructor(
        private http: Http,
        private router: Router,
        @Inject('BASE_URL') private baseUrl: string
    ) { }

    private options: any = {
        withCredentials: true
    };

    public async getChapterRatings(id: string): Promise<ChapterRatingModel[]> {
        let ratings: ChapterRatingModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetChapterRatings + id, this.options).toPromise()).json();
        return ratings;
    }

    public async createChapterRating(id: string, rating: ChapterRatingModel) {
        let response;
        try {
            response = await this.http.post(this.baseUrl + HttpQueryStrings.CreateChapterRating + id, rating, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }

    public async deleteChapterRating(id: string) {
        let response;
        try {
            response = await this.http.delete(this.baseUrl + HttpQueryStrings.DeleteChapterRating + id, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }
}