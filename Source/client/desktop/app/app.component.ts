import { Component, OnInit } from '@angular/core';
import { ProductComponent }  from './components/product/product.component';
import { AuthService } from './auth.service';
import { ProductsService } from './products.service';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
    selector: 'app-app',
    templateUrl: 'app/app.component.html',
    directives: [ProductComponent],
    providers: [AuthService, ProductsService]
})
export class AppComponent implements OnInit {

    constructor(private auth: AuthService,
                private router: Router,
                private route: ActivatedRoute,
                private productsService: ProductsService) { }

    userEmail(): string {
        return this.auth.userEmail();
    }

    isLoggedIn() {
        return this.auth.isLoggedIn();
    }

    isAdmin() {
        return this.auth.isAdmin();
    }

    logout() {
        this.auth.logout();
    }

    searchFor(term: string) {
        if (term) {
            this.router.navigate(['browse', 'search', term]);
            ProductsService.searchEvent.emit(term);
        } else {
            this.router.navigate(['browse']);
        }
    }

    ngOnInit(){}
}