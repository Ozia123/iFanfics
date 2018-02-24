import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { HttpQueryStrings } from '../helpers/HttpQueryStrings';
import { FanficModel } from '../models/FanficModel';
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

    public async getAll(): Promise<FanficModel[]> {
        let fanfics: FanficModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetAllFanfics, this.options).toPromise()).json();
        return fanfics;
    }

    public async getAllUsers(username: string): Promise<FanficModel[]> {
        let fanfics: FanficModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetUserFanfics + username, this.options).toPromise()).json();
        return fanfics;
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