/**
 * Product
 */
export class Product {
    id: string;
    subscribed: boolean; // if viewing user is subscribed

    constructor(public name: string,
                public price: number
                ) {}
}