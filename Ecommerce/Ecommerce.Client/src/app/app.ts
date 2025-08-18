import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Ecommerce.Client');

  constructor(http: HttpClient) {
    http.get('/api/Produtos').subscribe(dados => {
      console.log('Dados recebidos:', dados);
    });
  }
}
