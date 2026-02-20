import { Component, ChangeDetectionStrategy, inject } from '@angular/core';
import { AltQuestionStore } from '../alt-question-store';

@Component({
  selector: 'app-list-2',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [],
  providers: [AltQuestionStore], // Make me explain this.
  template: `
    <ul>
      @for (q of store.entities(); track q.id) {
        <li class="card bg-base-200 card-xl shadow-sm">
          <div class="card-body">
            <h2 class="card-title">{{ q.title }}</h2>
            <p>CONTENT HERE: {{ q.question }}</p>
            <div class="justify-end card-actions">
              <button class="btn btn-primary">I Have an Answer!</button>
            </div>
          </div>
        </li>
      }
    </ul>
  `,
  styles: ``,
})
export class List2 {
  store = inject(AltQuestionStore);
}
