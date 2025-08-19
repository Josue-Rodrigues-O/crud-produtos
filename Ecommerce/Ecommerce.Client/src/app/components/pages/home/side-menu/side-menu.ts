import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-side-menu',
  imports: [],
  templateUrl: './side-menu.html',
  styleUrl: './side-menu.css'
})
export class SideMenu {
  constructor(private router: Router) {

  }

  onClickMenuIten(route: string) {
    this.router.navigate([`/ecommerce/${route}`]);
  }
}
