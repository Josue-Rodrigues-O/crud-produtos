import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../../models/product';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private url = '/api/products';

  constructor(private http: HttpClient) { }

  create(product: Product) {
    return this.http.post(this.url, product);
  }

  getAll(filter: any) {
    let params = new HttpParams();

    Object.keys(filter).forEach(key => {
      const value = filter[key];
      if (value !== null && value !== undefined && value !== '') {
        params = params.set(key, String(value));
      }
    });

    return this.http.get<Product[]>(this.url, { params });
  }

  getById(id: string) {
    let url = `${this.url}/${id}`;
    return this.http.get<Product>(url);
  }

  update(id: string, product: Product) {
    let url = `${this.url}/${id}`;
    return this.http.put(url, product);
  }

  delete(id: string) {
    let url = `${this.url}/${id}`;
    return this.http.delete<Product>(url);
  }
}
