import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-products',
  imports: [],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  constructor(http: HttpClient) {
    http.get('/api/products').subscribe(dados => {
      console.log('Dados recebidos:', dados);
    });
  }
}
