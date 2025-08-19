import { Component } from '@angular/core';
import { ProductService } from '../../services/product/product.service';
import { Product } from '../../models/product';
import { ProductCreationForm } from "../../components/pages/products/product-creation-form/product-creation-form";
import { ProductEditingForm } from "../../components/pages/products/product-editing-form/product-editing-form";

@Component({
  selector: 'app-products',
  imports: [ProductCreationForm, ProductEditingForm],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  products: Product[] = [];

  constructor(productService: ProductService) {
    productService.getAll().subscribe({
      next: products => this.products = products
    });
  }
}
