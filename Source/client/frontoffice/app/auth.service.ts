import { Injectable } from '@angular/core';

import { Http, Headers, Response, RequestOptionsArgs } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subscriber } from 'rxjs/Subscriber';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {
    user: {email: string};

    constructor(private router: Router,
                private http: Http) { }

    isLoggedIn() {
        let result: boolean = !!localStorage.getItem('token');
        return result;
    }

    authorizeHeaders(headers: Headers) {
        headers.append('Authorization', "Bearer "+localStorage.getItem('token'));
    }

    getAuthorizationHeaderValue() {
        return "Bearer "+localStorage.getItem('token');
    }

    login(email: string, password: string): Observable<string> {
        let headers = new Headers();
        headers.append('Content-Type', 'application/x-www-form-urlencoded');
        let requestOptionsArgs: RequestOptionsArgs = {headers: headers};
        let encodedData: string = this.objectToQueryString({
            username: email,
            password: password,
            grant_type: 'password'
        });

        let result = new Observable<any>((sub: Subscriber<any>) => {
            this.http.post('http://localhost:8080/token', encodedData, requestOptionsArgs).subscribe(
                (r) => {
                    let token: string = r.json().access_token;
                    localStorage.setItem('token', token);
                    localStorage.setItem('user.email', email);

                    // who am i
                    let whoamiHeaders = new Headers();
                    whoamiHeaders.append('Authorization', "Bearer "+token);
                    this.http.get('http://localhost:8080/api/accounts/whoami', {headers: whoamiHeaders}).subscribe(
                        (res) => {
                            if (res.json().isAdmin) {
                                localStorage.setItem('user.admin', "admin");
                            }
                            
                            sub.next();
                        }
                    );
                },
                (e) => {
                    sub.error(e.json().error_description);
                }
            );
        });

        return result;
    }

    userEmail() {
        return localStorage.getItem('user.email');
    }

    logout() {
        localStorage.removeItem('token');
        localStorage.removeItem('user.email');
        localStorage.removeItem('user.admin');
        this.router.navigate(['login']);
    }

    register(email: string, password: string): Observable<string> {
        let apiObs = this.http.post('http://localhost:8080/api/accounts/register', {
            email: email,
            password: password
        });

        let result = new Observable<string>((subscriber: Subscriber<Response>) => {
            let errorMessages: string = '';

            apiObs.subscribe(
                () => {
                    subscriber.next();
                },
                (errorResponse: Response) => {
                    if (errorResponse.type !== 2) {
                        subscriber.error(errorResponse.text());
                    }

                    let errorGroups = errorResponse.json().modelState;
                    for (let prop in errorGroups) {
                        if (!errorGroups[prop].length || errorGroups[prop].length === 0) continue;
                        errorGroups[prop].forEach((err: string) => {
                            errorMessages += err;
                        });
                    }
                    subscriber.error(errorMessages);
                }
            );
        });

        return result;
    }

    updateProfile(profile: {email: string}) {
        let headers = new Headers();
        this.authorizeHeaders(headers);
        let opts: RequestOptionsArgs = {headers: headers};
        // headers.append('Content-Type', 'application/x-www-form-urlencoded');
        // let data = this.objectToQueryString({email: profile.email});
        let data = {email: profile.email};

        this.http.post('http://localhost:8080/api/accounts/changeEmail', data, opts).subscribe(r => {
            localStorage.setItem('user.email', profile.email);
        });
    }

    isAdmin() {
        return !!localStorage.getItem('user.admin');
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
