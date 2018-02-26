import { Component } from '@angular/core';
import { HttpFanficService } from '../../services/http.fanfic.service';
import { Router } from '@angular/router';

import { TagModel } from '../../models/TagModel';
import { GenreModel } from '../../models/GenreModel';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css'],
    providers: [HttpFanficService]
})
export class NavMenuComponent {
    public genres: GenreModel[] = [];
    public tags: TagModel[] = [];

    public query: string = '';
    public isValid: boolean = false;

    constructor(private router: Router, private httpFanficService: HttpFanficService) { }

    ngOnInit() {
        this.Initialize();
    }

    private async Initialize() {
        this.genres = await this.httpFanficService.getAllGenres();
        this.tags = await this.httpFanficService.getAllTags();
    }

    public checkValidation() {
        this.isValid = this.query != '';
    }

    public onSubmit() {
        if (this.isValid) {
            this.router.navigate(['/search/' + this.query]);
        }
    }
}
