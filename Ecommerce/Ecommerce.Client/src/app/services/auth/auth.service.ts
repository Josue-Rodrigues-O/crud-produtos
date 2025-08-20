import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private url = '/api/auth/login'
  constructor(private http: HttpClient) {

  }

  login(user: User = { username: 'admin', password: '#Adm1234' }) {
    return this.http.post<{ access_token: string }>(this.url, user);
  }

  setToken(token: string) {
    localStorage.setItem('token', token);
  }
  
  getToken() {
    localStorage.getItem('token');
  }
}
