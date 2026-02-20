import { Component, ChangeDetectionStrategy, signal, inject } from '@angular/core';

import {
  FormField,
  form,
  required,
  minLength,
  maxLength,
  validateStandardSchema,
  submit,
} from '@angular/forms/signals';

import { zQuestionSubmissionItem } from '../../shared/api/zod.gen';
import { QuestionStore } from '../question-store';
import { QuestionSubmissionItem } from '../types';

@Component({
  selector: 'app-questions-ask',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [FormField],
  template: `
    <form (submit)="handleSubmit($event)" novalidate>
      <fieldset class="fieldset">
        <legend class="fieldset-legend">What's your question?</legend>
        <label class="label" for="title"><span class="label-text font-medium">Title</span> </label>
        <input
          [formField]="form.title"
          type="text"
          class="input input-bordered"
          placeholder="Type here"
          id="title"
        />

        @if (form.title().invalid() && form.title().touched()) {
          <div class="alert alert-error">
            @for (error of form.title().errors(); track error) {
              <p>{{ error.message }}</p>
            }
          </div>
        }

        <label class="label" for="content"
          ><span class="label-text font-medium">Give us the deets</span>
        </label>
        <textarea
          [formField]="form.question"
          class="textarea"
          placeholder="Type here"
          id="content"
        ></textarea>
        <p class="label">Description of your question</p>

        @if (form.question().invalid() && form.question().touched()) {
          <div class="alert alert-error">
            @for (error of form.question().errors(); track error) {
              <p>{{ error.message }}</p>
            }
          </div>
        }
      </fieldset>
      <button
        type="submit"
        class="btn btn-primary"
        [attr.aria-disabled]="form().invalid() || form().submitting()"
      >
        Submit Question
      </button>
    </form>
  `,
  styles: `
    button[aria-disabled='true'] {
      background-color: gray;
      cursor: not-allowed;
    }
  `,
})
export class Ask {
  store = inject(QuestionStore);
  handleSubmit(event: SubmitEvent) {
    event.preventDefault();
    submit(this.form, async (f) => {
      const value = f().value();
      await this.store.addQuestion(value);

      f().reset();
    });
  }
  model = signal<QuestionSubmissionItem>({
    title: '',
    question: '',
    priority: 0,
  });

  // form = form(this.model, (schemata) => {
  //   required(schemata.title);
  //   minLength(schemata.title, 5);
  //   maxLength(schemata.title, 100);
  //   required(schemata.content);
  //   minLength(schemata.content, 10);
  //   maxLength(schemata.content, 1000);
  // });
  form = form(this.model, (schema) => {
    validateStandardSchema(schema, zQuestionSubmissionItem);
    // see styles.css and the input[required]::after rule - weird, but schema is different than form validation.
    // Jeff showed this, don't think he just "knew" how to do this. He didn't. This was about 6 hours of frustration.

    required(schema.title);
    required(schema.question);
  });
}
