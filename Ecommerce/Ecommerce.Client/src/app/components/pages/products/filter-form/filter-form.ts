import { Component, input, output } from '@angular/core';
import { Input } from "../../../reusable/input/input";
import { ProductFilterFields } from '../../../../models/product-filter-fields';
import { InputDecimal } from "../../../reusable/input-decimal/input-decimal";
import { Button } from "../../../reusable/button/button";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filter-form',
  imports: [Input, InputDecimal, Button, FormsModule],
  templateUrl: './filter-form.html',
  styleUrl: './filter-form.css'
})
export class FilterForm {
  filtered = output<ProductFilterFields>();
  filter = input.required<ProductFilterFields>();

  onClickFilter = () => {
    this.filtered.emit(this.filter());
  }
}
