import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Products } from './pages/products/products';
import { Auth } from './pages/auth/auth';
import { Welcome } from './pages/welcome/welcome';

export const routes: Routes = [
    { path: '', component: Auth },
    {
        path: 'ecommerce', component: Home, children: [
            { path: '', component: Welcome, pathMatch: 'full' },
            { path: 'produtos', component: Products }
        ]
    }
];
