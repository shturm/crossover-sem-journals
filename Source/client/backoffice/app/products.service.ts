import { Injectable, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

import {AuthService} from './auth.service';

import { Http, Response, RequestOptionsArgs, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subscriber } from 'rxjs/Subscriber';
import {Subject} from 'rxjs/Subject';

import { Product } from './product.model';
import { Paper } from './paper.model';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class ProductsService {

    // Observable string sources
    static searchEvent = new EventEmitter<string>();

    constructor(private router: Router,
        private http: Http,
        private auth: AuthService) { }

    findProduct(id: string): Observable<any> {
        let headers = new Headers();
        this.auth.authorizeHeaders(headers);
        let opts: RequestOptionsArgs = { headers: headers };

        let result = new Observable<Product>((sub: Subscriber<any>) => {
            this.http.get('http://localhost:8080/api/journals?id=' + encodeURIComponent(id), opts)
                .subscribe(journalResponse => {
                    this.http.get('http://localhost:8080/api/papers?journalId=' + encodeURIComponent(id), opts)
                        .subscribe(papersResponse => {
                            sub.next({
                                papers: papersResponse.json(),
                                journal: journalResponse.json()
                            });
                        });
                });
        });

        return result;
    }

    getProducts(term: string): Observable<Product[]> {
        let result = new Observable<Product[]>((sub: Subscriber<Product[]>) => {
            let headers = new Headers();
            this.auth.authorizeHeaders(headers);
            let opts: RequestOptionsArgs = { headers: headers };
            let url = 'http://localhost:8080/api/journals';
            if (term) {
                url += '?term=' + encodeURIComponent(term);
            }

            this.http.get(url, opts).subscribe(r => {
                sub.next(r.json());
            }, e => {
                console.log(e);
            });
        });

        return result;
    }

    createProduct(p: Product): Promise<any> {
        let headers = new Headers();
        this.auth.authorizeHeaders(headers);
        headers.append('Content-Type', 'application/json');
        let opts: RequestOptionsArgs = { headers: headers };
        // let data = this.objectToQueryString(p);
        let data = p;
        return this.http.post('http://localhost:8080/api/journals', data, opts).toPromise();
    }

    updateProduct(p: Product): Observable<any> {
        let result = new Observable<any>((sub: Subscriber<any>) => {
            let headers = new Headers();
            this.auth.authorizeHeaders(headers);
            let opts: RequestOptionsArgs = { headers: headers };

            this.http.put('http://localhost:8080/api/journals', p, opts).subscribe(r => {
                sub.next(r);
            }, e => {
                sub.error(e);
                console.log(e);
            });
        });

        return result;
    }

    deleteJournal(p: Product): Promise<any> {
            let headers = new Headers();
            this.auth.authorizeHeaders(headers);
            let opts: RequestOptionsArgs = { headers: headers };

            return this.http.delete('http://localhost:8080/api/journals?journalId=' + p.id, opts).toPromise();
    }

    addPaperToJournal(paper: Paper, journalId: number): Promise<any> {
        let authorizationHeaderValue = this.auth.getAuthorizationHeaderValue();
        let pr = new Promise((resolve, reject) => {
            
            let xhr = new XMLHttpRequest();
            let formData = new FormData();
            formData.append('paperFile', paper.file);
            formData.append('paperName', paper.name);
            formData.append('journalId', journalId);

            xhr.onreadystatechange = function (e) {
                if (4 == this.readyState) {
                    resolve();
                }
            };

            xhr.open('post', 'http://localhost:8080/api/papers');
            xhr.setRequestHeader("Authorization", authorizationHeaderValue);
            xhr.send(formData);
        });

        return pr;
    }

    deletePaper(paperId: number): Promise<any> {
        let headers = new Headers();
        this.auth.authorizeHeaders(headers);
        let opts: RequestOptionsArgs = { headers: headers };

        return this.http.delete('http://localhost:8080/api/papers?paperId=' + paperId, opts).toPromise();
    }

    getPaper(paperId: number): Promise<Paper> {
        let headers = new Headers();
        this.auth.authorizeHeaders(headers);
        let opts: RequestOptionsArgs = { headers: headers };
        
        let getPaperPromise = this.http.get('http://localhost:8080/api/papers?paperId=' + paperId, opts).toPromise();
        let result = new Promise((resolve, reject) => {
            getPaperPromise.then(paperRes => {
                resolve(paperRes.json());
            });
        });

        return result;
    }

    getPaperPage(paperId: number, pageNumber: number): Promise<string> {
        let headers = new Headers();
        this.auth.authorizeHeaders(headers);
        let opts: RequestOptionsArgs = { headers: headers };
        let requestData = {paperId: paperId, pageNumber: pageNumber};

        let promise = this.http.post('http://localhost:8080/api/papers/page', requestData, opts).toPromise();
        let result = new Promise((resolve, reject) => {
            promise.then((res) => {
                resolve(res.text().replace(/"/g, ''));
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