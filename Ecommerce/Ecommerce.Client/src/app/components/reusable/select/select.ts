import { Component, input, output } from '@angular/core';
import { SelectItem } from '../../../models/select-item';
import { InputBase } from '../../../models/input-base';

@Component({
  selector: 'app-select',
  imports: [],
  templateUrl: './select.html',
  styleUrl: './select.css'
})
export class Select implements InputBase {
  options = input.required<SelectItem[]>();
  func = input<() => void>(() => { });
  placeholder = input<string>('');
  value = input();
  valueChange = output<any>();
  isInvalidValue = false;
  messageError = '';

  onChange(event: Event) {
    const input = event.target as HTMLInputElement;

    if (input.value) {
      this.valueChange.emit(input.value);
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
