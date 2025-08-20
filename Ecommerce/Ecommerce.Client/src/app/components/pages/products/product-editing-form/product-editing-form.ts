import { Component, ElementRef, input, output, ViewChild } from '@angular/core';
import { Input } from "../../../reusable/input/input";
import { Product } from '../../../../models/product';
import { ProductService } from '../../../../services/product/product.service';
import { InputDecimal } from "../../../reusable/input-decimal/input-decimal";
import { Button } from "../../../reusable/button/button";
import { InputBase } from '../../../../models/input-base';
import { FieldValidation } from '../../../../models/field-validation';
import { RequestService } from '../../../../services/request/request.service';
import { DepartmentService } from '../../../../services/department/department.service';
import { SelectItem } from '../../../../models/select-item';
import { Select } from "../../../reusable/select/select";

@Component({
  selector: 'app-product-editing-form',
  imports: [Input, InputDecimal, Button, Select],
  templateUrl: './product-editing-form.html',
  styleUrl: './product-editing-form.css'
})
export class ProductEditingForm {
  @ViewChild('dialog') dialog!: ElementRef<HTMLDialogElement>;
  @ViewChild('descricao') inpDescricao!: InputBase;
  @ViewChild('departamento') inpDepartamento!: InputBase;
  @ViewChild('preco') inpPreco!: InputBase;
  departments!: SelectItem[];
  product: Product = {
    codigo: '',
    descricao: '',
    departamento: '',
    preco: 0,
  }
  private selectedProductId: string = '';

  constructor(
    private productService: ProductService,
    private requestService: RequestService,
    private departmentService: DepartmentService
  ) { }

  saved = output<void>();
  open(productId: string) {
    this.loadDepartments();
    this.selectedProductId = productId;
    this.productService
      .getById(productId)
      .subscribe({
        next: prod => this.product = prod,
        complete: () => this.dialog.nativeElement.showModal()
      });
  }

  close = () => {
    this.dialog.nativeElement.close();
  }

  save = () => {
    this.productService.update(this.selectedProductId, this.product).subscribe({
      next: dados => {
        console.log(dados)
      },
      error: res => {
        this.requestService.setErrorInInvalidFields(this.getFieldsValidation(), res.error.errors)
      },
      complete: () => {
        this.close()
        this.saved.emit()
      }
    });
  }

  private getFieldsValidation(): FieldValidation[] {
    return [
      { id: "descricao", field: this.inpDescricao },
      { id: "departamento", field: this.inpDepartamento },
      { id: "preco", field: this.inpPreco },
    ];
  }

  private loadDepartments() {
    this.departmentService
      .getAll()
      .subscribe({
        next: res => this.departments = res
      });
  }
}
