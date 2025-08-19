import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TopBar } from "../../components/pages/home/top-bar/top-bar";
import { SideMenu } from "../../components/pages/home/side-menu/side-menu";

@Component({
  selector: 'app-home',
  imports: [RouterOutlet, TopBar, SideMenu],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {

}
