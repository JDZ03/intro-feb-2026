import * as z from 'zod';
import { zQuestionSubmissionItem, zQuestionListItem } from '../shared/api/zod.gen';

export type QuestionSubmissionItem = z.infer<typeof zQuestionSubmissionItem>;

export type QuestionListItem = z.infer<typeof zQuestionListItem>;
