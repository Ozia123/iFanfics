import { LoginModel } from '../../models/LoginModel';
import { Component } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { HttpAuthService } from '../../services/http.auth.service';
import { AuthGuard } from '../../guards/auth.guard';

@Component({
    selector: 'auth/login',
    templateUrl: './login.component.html',
    providers: [HttpAuthService, AuthGuard]
})

export class LoginComponent {
    public loginModel: LoginModel = new LoginModel();
    public serverErrors: string;
    public isValid: boolean = false;

    constructor(private router: Router, private httpAuthService: HttpAuthService, private authGuard: AuthGuard) { }

    public onUsernameInput(username: string) {
        this.loginModel.Username = username;
        this.checkValidation();
    }

    public onPasswordInput(password: string) {
        this.loginModel.Password = password;
        this.checkValidation();
    }

    private checkValidation() {
        this.isValid = this.loginModel.Password != '' && this.loginModel.Username != '';
    }

    public async onSubmit() {
        const response = await this.httpAuthService.Login(this.loginModel);

        if (response.status == 200) {
            console.log('ok: login successed');
            await this.httpAuthService.getCurrentUser();
            this.router.navigate(['/home']);
        }
        if (response.status == 400) {
            this.serverErrors = 'username or password is incorrect';
        }
    }
}