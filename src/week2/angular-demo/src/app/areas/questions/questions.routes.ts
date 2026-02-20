import { Routes } from '@angular/router';
import { Questions } from './questions';
import { List } from './pages/list';
import { Ask } from './pages/ask';
import { QuestionStore } from './question-store';
import { AltQuestionStore } from './alt-question-store';
import { List2 } from './pages/list-2';
export const QuestionRoutes: Routes = [
  {
    path: '',
    component: Questions,
    providers: [QuestionStore],
    children: [
      {
        path: '',
        component: List,
      },
      {
        path: 'ask',
        component: Ask,
      },
      {
        path: 'list-2',
        component: List2,
      },
    ],
  },
];
