import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-auth',
  imports: [],
  templateUrl: './auth.html',
  styleUrl: './auth.css'
})
export class Auth {

  constructor(http: HttpClient) {
    http.post<{ access_token: string }>("/api/auth/login", { username: 'admin', password: '#Adm1234' })
      .subscribe(dados => localStorage.setItem('token', dados.access_token));
  }
}
