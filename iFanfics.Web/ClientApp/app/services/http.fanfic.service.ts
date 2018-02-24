import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { HttpQueryStrings } from '../helpers/HttpQueryStrings';
import { FanficModel } from '../models/FanficModel';
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
}