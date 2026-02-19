export type QuestionListItem = {
  id: string;
  title: string;
  content: string;
  submittedAnswers?: {
    id: string;
    content: string;
  }[];
};
