import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth.service';

@Component({
    selector: 'app-registerPage',
    templateUrl: 'app/pages/register/registerPage.component.html',
    directives: []
})
export class RegisterPageComponent {
    errorMessage: string;

    constructor(private auth: AuthService,
                private router: Router) {

    }

    register(credentials: any) {
        if (credentials.password !== credentials.repeatPassword) {
            this.errorMessage = "Passwords don't match";
            return;
        }

        this.auth.register(credentials.email, credentials.password).subscribe(
            () => {
                this.errorMessage = "";
                this.auth.login(credentials.email, credentials.password).subscribe(
                    ()=> {this.router.navigate(['browse']); },
                    ()=> {this.router.navigate(['login']); }
                );
            },
            (e: string) => {
                this.errorMessage = e;
            }
        );
    }
}
