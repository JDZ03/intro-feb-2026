import { patchState, signalStore, withHooks, withMethods } from '@ngrx/signals';
import { withEntities, setEntities } from '@ngrx/signals/entities';
import { QuestionListItem } from './types';
import { zGetQuestionsResponse } from '../shared/api/zod.gen';
// type BogusType = {
//   id: string;
//   title?: string | undefined;
//   content?: string | undefined;
//   submittedAnswers?:
//     | {
//         id?: string | undefined;
//         content?: string | undefined;
//       }[]
//     | null
//     | undefined;
// };
export const AltQuestionStore = signalStore(
  withEntities<QuestionListItem>(),
  withMethods((store) => {
    return {
      _load: async () => {
        try {
          const data = await fetch('/api/questions') // late bound call.
            .then((res) => res.json())
            .then((data) => zGetQuestionsResponse.parse(data)); // if you really don't trust apis, or if failure is not an option, VALIDATE the data.
          patchState(store, setEntities(data || []));
        } catch (ex) {
          // webService.notify(this thing is broke!)
          console.error('Could not validate the API data! What should we do?', ex);
        }
      },
    };
  }),
  withHooks({
    onInit: (store) => {
      store._load();
    },
  }),
);
