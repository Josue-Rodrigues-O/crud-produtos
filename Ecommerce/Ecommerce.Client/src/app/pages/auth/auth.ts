import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';
import { Button } from "../../components/reusable/button/button";

@Component({
  selector: 'app-auth',
  imports: [Button],
  templateUrl: './auth.html',
  styleUrl: './auth.css'
})
export class Auth {

  constructor(private router: Router, private authService: AuthService) {

  }

  onClickLogin = () => {
    this.authService.login().subscribe({
      next: dados => this.authService.setToken(dados.access_token),
      complete: () => this.router.navigate(['/ecommerce'])
    });
  }
}
