﻿import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { HttpQueryStrings } from '../helpers/HttpQueryStrings';
import { FanficModel } from '../models/FanficModel';
import { CommentsCountModel } from '../models/CommentsCountModel';
import { ChapterRatingValueModel } from '../models/ChapterRatingValueModel';
import { GenreModel } from '../models/GenreModel';
import { TagModel } from '../models/TagModel';
import { CreateFanficModel } from '../models/CreateFanficModel';

@Injectable()
export class HttpFanficService {
    constructor(
        private http: Http,
        private router: Router,
        @Inject('BASE_URL') private baseUrl: string
    ) { }

    private options: any = {
        withCredentials: true
    };
    
    public async getChapterRatingValue(id: string): Promise<number> {
        let count: CommentsCountModel = (await this.http.get(this.baseUrl + HttpQueryStrings.GetCommentsCount + id, this.options).toPromise()).json();
        return count.count;
    }

    public async getCommentsCount(id: string): Promise<number> {
        let val: ChapterRatingValueModel = (await this.http.get(this.baseUrl + HttpQueryStrings.GetChapterRatingValue + id, this.options).toPromise()).json();
        return val.ratingValue;
    }

    public async getAll(): Promise<FanficModel[]> {
        let fanfics: FanficModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetAllFanfics, this.options).toPromise()).json();
        return fanfics;
    }

    public async getAllByQuery(query: string): Promise<FanficModel[]> {
        let fanfics: FanficModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetAllFanficsByQuery + query, this.options).toPromise()).json();
        return fanfics;
    }

    public async getAllByGenre(genreName: string): Promise<FanficModel[]> {
        let fanfics: FanficModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetAllFanficsByGenre + genreName, this.options).toPromise()).json();
        return fanfics;
    }

    public async getAllByTag(tagName: string): Promise<FanficModel[]> {
        let fanfics: FanficModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetAllFanficsByTag + tagName, this.options).toPromise()).json();
        return fanfics;
    }

    public async getAllUsers(username: string): Promise<FanficModel[]> {
        let fanfics: FanficModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetUserFanfics + username, this.options).toPromise()).json();
        return fanfics;
    }

    public async getFanfic(id: string): Promise<FanficModel> {
        let fanfic: FanficModel = (await this.http.get(this.baseUrl + HttpQueryStrings.GetFanfic + id, this.options).toPromise()).json();
        return fanfic;
    }

    public async CreateFanfic(fanfic: CreateFanficModel) {
        let response;
        try {
            response = await this.http.post(this.baseUrl + HttpQueryStrings.CreateFanfic, fanfic, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }

    public async EditFanfic(fanfic: FanficModel) {
        let response;
        try {
            response = await this.http.put(this.baseUrl + HttpQueryStrings.EditFanfic, fanfic, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }

    public async DeleteFanfic(id: string) {
        let response;
        try {
            response = await this.http.delete(this.baseUrl + HttpQueryStrings.DeleteFanfic + id, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }

    public async getAllGenres(): Promise<GenreModel[]> {
        let genres: GenreModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetAllGenres, this.options).toPromise()).json();
        return genres;
    }

    public async getAllTags(): Promise<TagModel[]> {
        let tags: TagModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetAllTags, this.options).toPromise()).json();
        return tags;
    }

    public async AddTag(id: string, tag: TagModel) {
        let response;
        try {
            response = await this.http.put(this.baseUrl + HttpQueryStrings.AddTag + id, tag, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }

    public async DeleteTag(id: string, tag: TagModel) {
        let response;
        try {
            response = await this.http.put(this.baseUrl + HttpQueryStrings.DeleteTag + id, tag, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }

    public async CreateTag(tag: TagModel) {
        let response;
        try {
            response = await this.http.post(this.baseUrl + HttpQueryStrings.CreateTag, tag, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }
}