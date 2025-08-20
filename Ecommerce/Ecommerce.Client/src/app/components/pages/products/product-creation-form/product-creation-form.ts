import { Component, ElementRef, output, ViewChild } from '@angular/core';
import { Input } from "../../../reusable/input/input";
import { Product } from '../../../../models/product';
import { ProductService } from '../../../../services/product/product.service';
import { InputDecimal } from "../../../reusable/input-decimal/input-decimal";
import { Button } from "../../../reusable/button/button";
import { InputBase } from '../../../../models/input-base';
import { FieldValidation } from '../../../../models/field-validation';
import { RequestService } from '../../../../services/request/request.service';
import { Select } from "../../../reusable/select/select";
import { DepartmentService } from '../../../../services/department/department.service';
import { SelectItem } from '../../../../models/select-item';
import { map } from 'rxjs';
import { Department } from '../../../../models/department';

@Component({
  selector: 'app-product-creation-form',
  imports: [Input, InputDecimal, Button, Select],
  templateUrl: './product-creation-form.html',
  styleUrl: './product-creation-form.css'
})
export class ProductCreationForm {
  @ViewChild('dialog') dialog!: ElementRef<HTMLDialogElement>;
  @ViewChild('codigo') inpCodigo!: InputBase;
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

  constructor(
    private productService: ProductService,
    private requestService: RequestService,
    private departmentService: DepartmentService
  ) { }

  saved = output<void>();

  open = () => {
    this.loadDepartments();
    this.product = {
      codigo: '',
      descricao: '',
      departamento: String(this.departments[0].key),
      preco: 0,
    }
    this.dialog.nativeElement.showModal();
  }

  close = () => {
    this.dialog.nativeElement.close();
  }

  save = () => {
    this.productService.create(this.product).subscribe({
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
      { id: "codigo", field: this.inpCodigo },
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
