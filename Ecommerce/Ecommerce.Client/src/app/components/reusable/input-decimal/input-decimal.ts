import { Component, input, output } from '@angular/core';
import { InputBase } from '../../../models/input-base';

@Component({
  selector: 'app-input-decimal',
  imports: [],
  templateUrl: './input-decimal.html',
  styleUrl: './input-decimal.css'
})
export class InputDecimal implements InputBase {
  placeholder = input<string>('');
  value = input<number>();
  valueChange = output<number>();
  isInvalidValue = false;
  messageError = '';

  onInput(event: Event) {
    const input = event.target as HTMLInputElement;
    const num = parseFloat(input.value);

    if (!isNaN(num)) {
      this.valueChange.emit(num);
    }
  }

  onBlur(event: Event) {
    const input = event.target as HTMLInputElement;
    const num = parseFloat(input.value);

    if (!isNaN(num)) {
      input.value = num.toFixed(2);
      this.valueChange.emit(num);
    }
  }

  setInvalidState(message: string) {
    this.messageError = message;
    this.isInvalidValue = true;
  }

  setValidState() {
    this.isInvalidValue = false;
  }
}
