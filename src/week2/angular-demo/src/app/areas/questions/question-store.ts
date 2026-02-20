import { httpResource } from '@angular/common/http';
import { signalStore, withHooks, withMethods, withProps } from '@ngrx/signals';
import * as z from 'zod';
import { zGetQuestionsResponse } from '../shared/api/zod.gen';
import { QuestionSubmissionItem } from './types';
type GetQuestionResponse = z.infer<typeof zGetQuestionsResponse>;
export const QuestionStore = signalStore(
  withProps(() => {
    return {
      questionResource: httpResource<GetQuestionResponse>(() => '/api/questions'),
    };
  }),
  withMethods((store) => {
    return {
      addQuestion: async (question: QuestionSubmissionItem) => {
        // POST to the API to add a question
        await fetch('/api/questions', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(question),
        });
        // Reload the question list
        // this is good actually.
        store.questionResource.reload();
      },
    };
  }),
  withHooks({
    onInit(store) {
      console.log('The QuestionStore has been initialized.');
      // set a timer, ever X minutes or so, do this:
      // setInterval(() => {
      //   store.questionResource.reload();
      // }, 5000);
    },
    onDestroy(store) {
      console.log('The QuestionStore is being destroyed.');
    },
  }),
);
