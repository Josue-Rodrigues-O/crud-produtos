import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product/product.service';
import { Product } from '../../models/product';
import { ProductCreationForm } from "../../components/pages/products/product-creation-form/product-creation-form";
import { ProductEditingForm } from "../../components/pages/products/product-editing-form/product-editing-form";
import { Button } from "../../components/reusable/button/button";
import { FilterForm } from "../../components/pages/products/filter-form/filter-form";
import { ProductFilterFields } from '../../models/product-filter-fields';
import { ActivatedRoute, Router } from '@angular/router';
import { StatusLabelPipe } from "../../pipes/StatusLabelPipe";
import { DescriptionFromListPipe } from "../../pipes/DepartmentLabelPipe";
import { SelectItem } from '../../models/select-item';
import { DepartmentService } from '../../services/department/department.service';
import { PriceLabelPipe } from "../../pipes/PriceLabelPipe";

@Component({
  selector: 'app-products',
  imports: [ProductCreationForm, ProductEditingForm, Button, FilterForm, StatusLabelPipe, DescriptionFromListPipe, PriceLabelPipe],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products implements OnInit {
  products: Product[] = [];
  departments!: SelectItem[];
  filter!: ProductFilterFields;

  constructor(private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router,
    private departmentService: DepartmentService) {
  }

  ngOnInit() {
    this.loadDepartments();
    this.route.queryParams.subscribe(params => {
      this.filter = {
        codigo: params['codigo'] || '',
        descricao: params['descricao'] || '',
        departamento: params['departamento'] || '',
        precoInicial: params['precoInicial'] || 0,
        precoFinal: params['precoFinal'] || 0,
        incluirItensInativos: params['incluirItensInativos'] == 'true'
      };
      this.updateList()
    });
  }

  updateFilter(filter: ProductFilterFields) {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: filter,
      queryParamsHandling: 'merge'
    });
  }

  updateList() {
    this.productService.getAll(this.filter).subscribe({
      next: products => this.products = products
    });
  }

  deleteProduct(id: string) {
    this.productService.delete(id).subscribe({
      complete: () => this.updateList()
    });
  }

  private loadDepartments() {
    this.departmentService
      .getAll()
      .subscribe({
        next: res => this.departments = res
      });
  }
}
