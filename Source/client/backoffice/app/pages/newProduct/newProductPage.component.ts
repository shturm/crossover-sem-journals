import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ProductComponent } from '../../components/product/product.component';
import { Product } from '../../product.model';
import { ProductsService } from '../../products.service';

@Component({
    selector: 'app-newProductPage',
    templateUrl: 'app/pages/newProduct/newProductPage.component.html',
    directives: [ProductComponent],
    providers: [ProductsService]
})
export class NewProductPageComponent { 
    product: Product = new Product('',0);

    constructor(private productsService: ProductsService,
                private router: Router) { }

    createProduct(p: Product) {
        this.productsService.createProduct(p).then(()=>{
            this.router.navigate(['browse']);
        }, ()=>{
            console.log('could not create journal');
        });
        
    }
}