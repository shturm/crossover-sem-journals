import { Component } from '@angular/core';
import { Router } from '@angular/router';
// import { FormBuilder, Validators } from '@angular/forms';
import { AuthService} from '../../auth.service';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'app-loginPage',
    templateUrl: 'app/pages/login/loginPage.component.html',
    directives: []
})
export class LoginPageComponent { 
    errorMessage: string = "";

    constructor(private auth: AuthService,
                private router: Router) {
       
    }
    
    logIn(credentials: any) {
        this.auth.login(credentials.email, credentials.password).subscribe(
            () => {
                this.router.navigate(['browse']);
            },
            (e: string) => {
                this.errorMessage = e;
            }
        );
    }
}
