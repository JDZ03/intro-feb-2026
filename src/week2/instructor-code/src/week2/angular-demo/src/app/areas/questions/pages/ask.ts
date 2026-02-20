import { Component, ChangeDetectionStrategy, signal } from '@angular/core';

import {
  FormField,
  form,
  required,
  minLength,
  maxLength,
  validateStandardSchema,
  submit,
} from '@angular/forms/signals';
import * as z from 'zod';

type QuestionSubmissionItem = z.infer<typeof zQuestionSubmissionItem>;
import { zQuestionSubmissionItem } from '../../shared/api/zod.gen';

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
          [formField]="form.content"
          class="textarea"
          placeholder="Type here"
          id="content"
        ></textarea>
        <p class="label">Description of your question</p>

        @if (form.content().invalid() && form.content().touched()) {
          <div class="alert alert-error">
            @for (error of form.content().errors(); track error) {
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
    input[required]::after {
      content: ' *';
      color: red;
    }
  `,
})
export class Ask {
  handleSubmit(event: SubmitEvent) {
    event.preventDefault();
    submit(this.form, async (f) => {
      const value = f().value();
      console.log('Form submitted with value:', value);
      await new Promise((resolve) => setTimeout(resolve, 1000));
    });
  }
  model = signal<QuestionSubmissionItem>({
    title: '',
    content: '',
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
    required(schema.title);
    required(schema.content);
  });
}
