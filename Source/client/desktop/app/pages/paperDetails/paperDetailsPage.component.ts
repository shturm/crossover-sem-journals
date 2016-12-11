import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AuthService } from '../../auth.service';

import {DomSanitizationService} from '@angular/platform-browser';

import {ProductComponent} from '../../components/product/product.component';
import { Product} from '../../product.model';
import { Paper} from '../../paper.model';
import { Page} from '../../page.model';

import { ProductsService} from '../../products.service';

@Component({
    moduleId: module.id,
    templateUrl: 'paperDetailsPage.component.html',
    directives: [ProductComponent],
    providers: [ProductsService]
})
export class PaperDetailsPageComponent implements OnInit {
    paper: Paper;
    paperPage: Page;
    b64: string;
    b64sanitized: any;

    constructor(private route: ActivatedRoute,
                private router: Router, 
                private auth: AuthService,
                private productsService: ProductsService,
                private sanitizer: DomSanitizationService) {    }

    ngOnInit() {
        // this.product = this.productsService.findProduct(id);
        this.route.params.forEach((params: Params) => {
            let pageNumber = Number.parseInt(params['pageNumber']);
            let paperId = Number.parseInt(params['paperId']);

            this.updateData(paperId, pageNumber);
        });
    }

    private updateData(paperId: number, pageNumber: number) {
            this.productsService.getPaper(paperId).then(paper => {
                this.paper = paper;
            });

            this.productsService.getPaperPage(paperId, pageNumber).then(pageImageb64 => {
                this.paperPage = new Page(0,pageNumber,pageImageb64);
                this.b64 = pageImageb64;
                this.b64sanitized = this.sanitizer.bypassSecurityTrustUrl("data:image/jpeg;base64,"+this.b64);
            });
    }

    nextPage() {
        this.updateData(this.paper.id, this.paperPage.pageNumber+1);
    }

    previousPage() {
        this.updateData(this.paper.id, this.paperPage.pageNumber-1);
    }

}