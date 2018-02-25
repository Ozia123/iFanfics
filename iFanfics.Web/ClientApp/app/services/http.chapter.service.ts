import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

import { HttpQueryStrings } from '../helpers/HttpQueryStrings';
import { ChapterModel } from '../models/ChapterModel';

@Injectable()
export class HttpChapterService {
    constructor(
        private http: Http,
        private router: Router,
        @Inject('BASE_URL') private baseUrl: string
    ) { }

    private options: any = {
        withCredentials: true
    };

    public async getFanficChapters(id: string): Promise<ChapterModel[]> {
        console.log('getting chapters');
        let chapters: ChapterModel[] = (await this.http.get(this.baseUrl + HttpQueryStrings.GetFanficChapters + id, this.options).toPromise()).json();
        return chapters;
    }

    public async createChapter(id: string, chapter: ChapterModel) {
        console.log('id: ' + id + '\nchapter: ' + chapter.title);
        let response;
        try {
            response = await this.http.post(this.baseUrl + HttpQueryStrings.CreateChapter + id, chapter, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }

    public async editChapter(id: string, chapter: ChapterModel) {
        let response;
        try {
            response = await this.http.put(this.baseUrl + HttpQueryStrings.EditChapter + id, chapter, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }

    public async DeleteChapter(id: string) {
        let response;
        try {
            response = await this.http.delete(this.baseUrl + HttpQueryStrings.DeleteChapter + id, this.options).toPromise();
            return response;
        } catch (ex) {
            return ex;
        }
    }
}