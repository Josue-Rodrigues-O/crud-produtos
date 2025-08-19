import { Component, input, output } from '@angular/core';
import { InputBase } from '../../../models/input-base';

@Component({
  selector: 'app-input',
  imports: [],
  templateUrl: './input.html',
  styleUrl: './input.css'
})
export class Input implements InputBase {
  placeholder = input<string>('');
  value = input<string>();
  valueChange = output<string>();
  isInvalidValue = false;
  messageError = '';

  onInput(event: Event) {
    const newValue = (event.target as HTMLInputElement).value;
    this.valueChange.emit(newValue);
  }

  setInvalidState(message: string) {
    this.messageError = message;
    this.isInvalidValue = true;
  }

  setValidState() {
    this.isInvalidValue = false;
  }
}
