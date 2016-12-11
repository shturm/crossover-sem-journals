import { Component, OnInit } from '@angular/core';

import { UsersService } from '../../users.service';
import { User } from '../../user.model';

@Component({
    moduleId: module.id,
    templateUrl: 'usersPage.component.html',
    providers: [UsersService]
})
export class UsersPageComponent implements OnInit {
    users: Array<User>;
    
    constructor(private usersService: UsersService) { }

    ngOnInit() {
        this.usersService.getUsers().subscribe(users=> {
            this.users = users;
        });
     }

    setUserAdmin(email: string, flag: boolean) {
        let msg: string = 'Promote user to admin ?';
        if (!flag) msg = 'Demote user from admin ?';

        if (confirm(msg)) {
            this.usersService.setUserAdmin(email, flag).subscribe(() => {
                this.usersService.getUsers().subscribe(users=> {
                    this.users = users;
                });
            });
        }

        
    }

}