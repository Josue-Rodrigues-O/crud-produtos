import { Component, input } from '@angular/core';

@Component({
  selector: 'app-button',
  imports: [],
  templateUrl: './button.html',
  styleUrl: './button.css'
})
export class Button {
  type = input('button');
  func = input<() => void>(() => { });
  constructor() { }

  protected onClickButton() {
    let funcao = this.func();
    funcao();
  }
}
