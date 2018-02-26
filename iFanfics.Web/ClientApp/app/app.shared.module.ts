import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, CanActivate } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { AuthComponent } from './components/auth/auth.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { FanficCreateComponent } from './components/fanfic/create/fanfic-create.component';
import { UserComponent } from './components/users/user.component';
import { FanficComponent } from './components/fanfic/fanfic.component';
import { FanficEditComponent } from './components/fanfic/edit/fanfic-edit.component';

import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/mergeMap';

import { HttpAuthService } from './services/http.auth.service';
import { AuthGuard } from './guards/auth.guard';
import { LoginGuard } from './guards/login.guard';
import { AdminGuard } from './guards/admin.guard';
import { OwnerGuard } from './guards/owner.guard';
import { HttpChapterService } from "./services/http.chapter.service";
import { HttpFanficService } from "./services/http.fanfic.service";
import { HttpCommentService } from './services/http.comment.service';
import { HttpChapterRatingService } from './services/http.chapter.rating.service';
import { HttpCommentRatingService } from './services/http.comment.rating.service';
import { HttpUserService } from "./services/http.user.service";
import { YouComponent } from "./components/you/you.component";
import { FanficShortComponent } from './components/fanfic/fanfic-short/fanfic-short.component';
import { CommentComponent } from './components/fanfic/comment/comment.component';
import { FanficsByGenreComponent } from './components/fanfic/by-genre/fanfics-by-genre.component';
import { FanficsByTagComponent } from './components/fanfic/by-tag/fanfics-by-tag.component';
import { FanficsByQueryComponent } from './components/fanfic/search/fanfics-by-query.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        AuthComponent,
        RegisterComponent,
        LoginComponent,
        FanficCreateComponent,
        UserComponent,
        FanficComponent,
        FanficEditComponent,
        YouComponent,
        FanficShortComponent,
        CommentComponent,
        FanficsByGenreComponent,
        FanficsByTagComponent,
        FanficsByQueryComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent, data: { title: 'iFanfics: HomePage' } },
            {
                path: 'auth/register',
                component: RegisterComponent,
                canActivate: [LoginGuard],
                data: { title: 'Register' }
            },
            {
                path: 'auth/login',
                component: LoginComponent,
                canActivate: [LoginGuard],
                data: { title: 'Login' }
            },
            {
                path: 'you',
                component: YouComponent,
                canActivate: [AuthGuard],
                data: { title: 'Your Profile' }
            },
            {
                path: 'fanfic/:id',
                component: FanficComponent,
                data: { title: 'Fanfic' }
            },
            {
                path: 'new/fanfic/create',
                component: FanficCreateComponent,
                canActivate: [AuthGuard],
                data: { title: 'Create new fanfic' }
            },
            {
                path: 'edit/fanfic/:id',
                component: FanficEditComponent,
                canActivate: [OwnerGuard],
                data: { title: 'Edit fanfic' }
            },
            {
                path: 'user/:id',
                component: UserComponent,
                data: { title: 'User profile' }
            },
            {
                path: 'bygenre/:id',
                component: FanficsByGenreComponent,
                data: { title: 'Results by genre' }
            },
            {
                path: 'bytag/:id',
                component: FanficsByTagComponent,
                data: { title: 'Results by tag'}
            },
            {
                path: 'search/:id',
                component: FanficsByQueryComponent,
                data: { title: 'Search' }
            },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        HttpAuthService,
        HttpChapterService,
        HttpFanficService,
        HttpCommentService,
        HttpChapterRatingService,
        HttpCommentRatingService,
        HttpUserService,
        AuthGuard,
        LoginGuard,
        AdminGuard,
        OwnerGuard
    ]
})
export class AppModuleShared {
}
