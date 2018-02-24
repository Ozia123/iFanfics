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
    public ModelIsValid: Boolean = false;

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
        }
        else {
            this.PasswordsDoesNotMatchMsg = '';
        }
    }

    public async onSubmit() {
        this.newUser.PictureURL = 'https://firebasestorage.googleapis.com/v0/b/ifanfics-3917e.appspot.com/o/default.png?alt=media&token=91bfd7a7-dbe6-4ffe-a66e-96811611e248';
        const response = await this.httpAuthService.Registration(this.newUser);

        if (response.status == 200) {
            this.router.navigate(['/auth/login']);
            await this.httpAuthService.getCurrentUser();
        }
        if (response.status == 400) {
            const data = response.json();
            this.ServerErrors = JSON.stringify(data);
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