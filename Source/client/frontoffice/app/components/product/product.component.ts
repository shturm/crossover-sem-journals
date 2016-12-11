import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Product } from '../../product.model';
import {Paper} from '../../paper.model';

import {CORE_DIRECTIVES, FORM_DIRECTIVES, NgClass, NgStyle} from '@angular/common';

@Component({
    selector: 'app-product',
    templateUrl: 'app/components/product/product.component.html',
})
export class ProductComponent implements OnInit{
    @Input()
    product: Product;

    @Input()
    papers: Array<Paper>;

    @Input()
    editable: boolean = true;
    
    @Output()
    onSubmit = new EventEmitter<Product>();

    @Output()
    onSubscribe = new EventEmitter<Product>();

    @Output()
    onUnsubscribe = new EventEmitter<Product>();

    @Output()
    onDeleteJournal = new EventEmitter<Product>();

    @Output()
    onSubmitNewPaper = new EventEmitter<Paper>();

    @Output()
    onDeletePaper = new EventEmitter<Paper>();

    @Output()
    onReadPaper = new EventEmitter<Paper>();

    newPaper: Paper = new Paper(0, '', 0);

    onFileChange(event: any) { // or Event
        this.newPaper.file = event.srcElement.files[0];
    }

    submitProduct() {
        this.onSubmit.emit(this.product);
        this.product = new Product('',0);
    }

    subscribe() {
        if (confirm('Subscribe ?'))
        {
            this.onSubscribe.emit(this.product);
        }
    }

    unsubscribe() {
        if (confirm('Unsubscribe ?'))
        {
            this.onUnsubscribe.emit(this.product);
        }
    }

    deletePaper(paper: Paper){
        if (confirm('Are you sure you want to delete the paper ?'))
        {
            this.onDeletePaper.emit(paper);
        }
    }

    readPaper(paper: Paper) {
        this.onReadPaper.emit(paper);
    }

    deleteJournal(journal: Product) {
        if (confirm('Are you sure you want to delete the journal ?'))
        {
            this.onDeleteJournal.emit(journal);
        }
    }

    submitNewPaper(formData: any) {
        this.onSubmitNewPaper.emit(this.newPaper);
    }

    ngOnInit() {
       
    }
}