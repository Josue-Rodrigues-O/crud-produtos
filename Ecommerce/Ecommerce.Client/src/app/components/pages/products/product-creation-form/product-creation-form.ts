import { Component, ElementRef, output, ViewChild } from '@angular/core';
import { Input } from "../../../reusable/input/input";
import { Product } from '../../../../models/product';
import { ProductService } from '../../../../services/product/product.service';
import { InputDecimal } from "../../../reusable/input-decimal/input-decimal";
import { Button } from "../../../reusable/button/button";
import { InputBase } from '../../../../models/input-base';
import { FieldValidation } from '../../../../models/field-validation';
import { RequestService } from '../../../../services/request/request.service';

@Component({
  selector: 'app-product-creation-form',
  imports: [Input, InputDecimal, Button],
  templateUrl: './product-creation-form.html',
  styleUrl: './product-creation-form.css'
})
export class ProductCreationForm {
  @ViewChild('dialog') dialog!: ElementRef<HTMLDialogElement>;
  @ViewChild('codigo') inpCodigo!: InputBase;
  @ViewChild('descricao') inpDescricao!: InputBase;
  @ViewChild('departamento') inpDepartamento!: InputBase;
  @ViewChild('preco') inpPreco!: InputBase;
  product: Product = {
    codigo: '',
    descricao: '',
    departamento: '',
    preco: 0,
  }

  constructor(private productService: ProductService, private requestService: RequestService) { }

  saved = output<void>();

  open = () => {
    this.product = {
      codigo: '',
      descricao: '',
      departamento: '',
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
}
