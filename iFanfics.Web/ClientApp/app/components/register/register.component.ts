import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import { Router } from '@angular/router';
import { RegisterModel } from '../../models/RegisterModel';
import { HttpAuthService } from '../../services/http.auth.service';

@Component({
    selector: 'auth/register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})

export class RegisterComponent {
    public newUser: RegisterModel;
    public PasswordsDoesNotMatchMsg: string = '';
    public ServerErrors: string;
    public isValid: boolean = false;
    public passwordsMatch: boolean = false;

    //private http: Http, private router: Router, @Inject('BASE_URL') baseUrl: string
    constructor(private router: Router, private httpAuthService: HttpAuthService) {
        this.newUser = new RegisterModel();
    }

    public onUsernameInput(username: string) {
        this.newUser.UserName = username;
    }

    public onEmailInput(email: string) {
        this.newUser.Email = email;
    }

    public onPasswordInput(password: string) {
        this.newUser.Password = password;
    }

    public onConfirmPasswordInput(confirmPassword: string) {
        this.newUser.ConfirmPassword = confirmPassword;
        if (this.newUser.Password != this.newUser.ConfirmPassword) {
            this.PasswordsDoesNotMatchMsg = 'Passwords does not match';
            this.passwordsMatch = false;
        }
        else {
            this.PasswordsDoesNotMatchMsg = '';
            this.passwordsMatch = true;
        }
    }

    private checkValidation() {
        this.isValid = this.newUser.Password != ''
            && this.newUser.UserName != ''
            && this.newUser.ConfirmPassword != ''
            && this.newUser.Email != '';
    }

    public async onSubmit() {
        this.newUser.PictureURL = 'https://www.cumtn.com/wp-content/plugins/all-in-one-seo-pack/images/default-user-image.png';
        const response = await this.httpAuthService.Registration(this.newUser);

        if (response.status == 200) {
            this.router.navigate(['/auth/login']);
            await this.httpAuthService.getCurrentUser();
        }
        if (response.status == 400) {
            const data = response.json();
            if (JSON.stringify(data) == '{}') {
                this.ServerErrors = 'username is already in use';
            }
            else {
                this.ServerErrors = 'make sure you signed up fields correctly, password must have at least one uppercase and non-alphabetic item and should be 8 or more characters long';
            }
        }
    }
}

interface ErrorData {
    key: any;
    value: any;
}

interface ErrorDataRoot {
    string: ErrorData[];
}