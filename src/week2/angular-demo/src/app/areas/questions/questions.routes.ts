import { Routes } from '@angular/router';
import { Questions } from './questions';
import { List } from './pages/list';
export const QuestionRoutes: Routes = [
  {
    path: '',
    component: Questions,
    children: [
      {
        path: '',
        component: List,
      },
    ],
  },
];
