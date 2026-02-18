import { Routes } from '@angular/router';
import { Home } from './home';
import { HomePage } from './pages/home';
import { SignalsPage } from './pages/signals';
import { CounterStore } from './counter-store';

export const BasicsRoutes: Routes = [
  {
    path: '',
    component: Home,
    // builder.services.addscoped
    providers: [CounterStore],
    children: [
      {
        path: 'dashboard',
        component: HomePage,
      },
      {
        path: 'signals',
        component: SignalsPage,
      },
      {
        path: '**',
        redirectTo: 'dashboard',
      },
    ],
  },
];
