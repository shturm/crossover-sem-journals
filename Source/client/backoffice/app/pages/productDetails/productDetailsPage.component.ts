import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AuthService } from '../../auth.service';

import {ProductComponent} from '../../components/product/product.component';
import { Product} from '../../product.model';
import { Paper} from '../../paper.model';
import { ProductsService} from '../../products.service';

@Component({
    moduleId: module.id,
    templateUrl: 'productDetailsPage.component.html',
    directives: [ProductComponent],
    providers: [ProductsService]
})
export class ProductDetailsPageComponent implements OnInit {
    id: string;
    product: Product;
    papers: Array<any>;


    constructor(private route: ActivatedRoute,
                private router: Router, 
                private auth: AuthService,
                private productsService: ProductsService) {    }

    ngOnInit() {
        // let id = this.route.params._value.id;
        // this.product = this.productsService.findProduct(id);
        this.route.params.forEach((params: Params) => {
            let id = params['id'];
            this.productsService.findProduct(id).subscribe(data => {
                this.product = data.journal;
                this.papers = data.papers;
            });
        });

    }

    updateProduct(p: Product) {
        this.productsService.updateProduct(p).subscribe(() => {
            this.router.navigate(['browse']);
        });
    }

    createNewPaper(paper: Paper) {
        this.productsService.addPaperToJournal(paper, Number.parseInt(this.product.id))
        .then(() => {
            this.ngOnInit();
        });
    }

    deletePaper(paper: Paper) {
        this.productsService.deletePaper(paper.id).then(() => {this.ngOnInit();});
    }

    deleteJournal(journal: Product) {
        this.productsService.deleteJournal(journal).then(() => {
            this.router.navigate(['browse']);
        });
    }

    goToPaper(paper: Paper) {
        // alert("navigating to paper "+paper.id);
        this.router.navigate(['read', paper.id, 1]);
    }

    userIsAdmin() {
        return this.auth.isAdmin();
    }

}
