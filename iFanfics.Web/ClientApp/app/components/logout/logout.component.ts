import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from '../../models/LoginModel';
import { HttpAuthService } from '../../services/http.auth.service';

@Component({
    selector: 'auth/logout',
    template: '<p>logout</p>'
})

export class LogoutComponent {
    constructor(private router: Router, private httpAuthService: HttpAuthService) { this.onSubmit(); }

    public async onSubmit() {
        const response = await this.httpAuthService.Logout();
        console.log('loged out');
        this.httpAuthService.getCurrentUser();
        this.router.navigate(['/home']);
    }
}