import { Component } from '@angular/core';
import {AuthService} from '../../auth.service';

@Component({
    selector: 'app-profilePage',
    templateUrl: 'app/pages/profile/profilePage.component.html',
    directives: []
})
export class ProfilePageComponent {
    email: string;

    constructor(private auth: AuthService) {
        this.email = this.auth.userEmail();
    }

    updateProfile(profile: {email: string}) {
        this.auth.updateProfile(profile);
    }
}