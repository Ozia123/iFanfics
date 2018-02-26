import { Component } from '@angular/core';
import { HttpFanficService } from '../../services/http.fanfic.service';

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

    constructor(private httpFanficService: HttpFanficService) { }

    ngOnInit() {
        this.Initialize();
    }

    private async Initialize() {
        this.genres = await this.httpFanficService.getAllGenres();
        this.tags = await this.httpFanficService.getAllTags();
    }
}
