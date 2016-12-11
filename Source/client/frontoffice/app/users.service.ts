import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptionsArgs } from '@angular/http';

import {Observable } from 'rxjs/Observable';
import {Subscriber } from 'rxjs/Subscriber';

import { User } from './user.model';
import {AuthService} from './auth.service';

@Injectable()
export class UsersService {
    static users: {email: string, admin: boolean}[] = [
        {email: 'gosho@pochivka.com', admin: true},
        {email: 'qvkata@dlg.com', admin: false},
        {email: 'toncho@gulub.com', admin: false},
        {email: 'mirko@cropcop.com', admin: false},
    ];

    constructor(private http: Http,
                private auth: AuthService) { }

    getUsers(): Observable<User[]> {
        let result = new Observable<User[]>((sub: Subscriber<User[]>) => {
            let headers = new Headers();
            this.auth.authorizeHeaders(headers);
            let opts: RequestOptionsArgs = {headers: headers};

            this.http.get('http://localhost:8080/api/accounts/getall', opts).subscribe(r => {
                let users: User[] = r.json().map((userDto:any)=> {
                    return {
                        id: userDto.id,
                        email: userDto.email,
                        isAdmin: userDto.isAdmin
                    };
                });
                sub.next(users);
            });
        });

        return result;
    }

    setUserAdmin(email: string, flag: boolean): Observable<any> {
        let result = new Observable<any>((sub: Subscriber<any>) => {
            let headers = new Headers();
            this.auth.authorizeHeaders(headers);
            // headers.append('Content-Type', 'application/x-www-form-urlencoded');
            let opts: RequestOptionsArgs = {headers: headers};
            let data = {email: email, flag: flag};

            this.http.post('http://localhost:8080/api/accounts/setAdmin', data, opts).subscribe(r => {
                sub.next();
            }, () => {
                sub.error();
            });
        });

        return result;
    }

    private objectToQueryString(obj: any) {
        let parts: any[] = [];
        for (let i in obj) {
            if (obj.hasOwnProperty(i)) {
                parts.push(encodeURIComponent(i) + '=' + encodeURIComponent(obj[i]));
            }
        }
        return parts.join('&');
    }
}