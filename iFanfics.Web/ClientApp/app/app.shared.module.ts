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
import { LogoutComponent } from './components/logout/logout.component';

import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/mergeMap';

import { HttpAuthService } from './services/http.auth.service';
import { AuthGuard } from './guards/auth.guard';
import { LoginGuard } from './guards/login.guard';
import { AdminGuard } from './guards/admin.guard';
import { HttpFanficService } from "./services/http.fanfic.service";
import { YouComponent } from "./components/you/you.component";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        AuthComponent,
        RegisterComponent,
        LoginComponent,
        LogoutComponent,
        YouComponent
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
                path: 'users/:username',
                // component
                data: { title: 'user' }
            },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        HttpAuthService,
        HttpFanficService,
        AuthGuard,
        LoginGuard,
        AdminGuard
    ]
})
export class AppModuleShared {
}