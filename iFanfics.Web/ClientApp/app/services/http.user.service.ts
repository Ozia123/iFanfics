import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { HttpQueryStrings } from '../helpers/HttpQueryStrings';
import { CurrentUserModel } from "../models/CurrentUserModel";

@Injectable()
export class HttpUserService {
    constructor(
        private http: Http,
        private router: Router,
        @Inject('BASE_URL') private baseUrl: string
    ) { }

    private options: any = {
        withCredentials: true
    };

    public async getUserProfile(username: string): Promise<CurrentUserModel> {
        let user: CurrentUserModel = (await this.http.get(this.baseUrl + HttpQueryStrings.GetUserProfile + username, this.options).toPromise()).json();
        return user;
    }
}