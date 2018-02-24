import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { HttpQueryStrings } from '../helpers/HttpQueryStrings'
import { RegisterModel } from '../models/RegisterModel'
import { LoginModel } from '../models/LoginModel'
import { CurrentUserModel } from "../models/CurrentUserModel";

@Injectable()
export class HttpAuthService {
    constructor(
        private http: Http,
        private router: Router,
        @Inject('BASE_URL') private baseUrl: string
    ) { }

    private options: any = {
        withCredentials: true
    };

    public currentUser: CurrentUserModel = new CurrentUserModel();

    public async getCurrentUser() {
        this.currentUser = (await this.http.get(this.baseUrl + HttpQueryStrings.CurrentUser, this.options).toPromise()).json();
    }

    public async Logout() {
        await this.http.get(this.baseUrl + HttpQueryStrings.LogoutUser, this.options).toPromise();
    }

    public async Login(loginUser: LoginModel) {
        let response;
        try {
            response = await this.http.post(this.baseUrl + HttpQueryStrings.LoginUser, loginUser, this.options).toPromise();
            return response;
        } catch (ex) {
            console.log(JSON.stringify(ex));
            return ex;
        }
    }

    public async Registration(registrationUser: RegisterModel) {
        let response;
        try {
            response = await this.http.post(this.baseUrl + HttpQueryStrings.RegiserUser, registrationUser, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }
}