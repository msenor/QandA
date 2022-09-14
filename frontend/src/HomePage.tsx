import React from 'react';
import { QuestionList } from './QuestionList';
import { getUnansweredQuestions } from './QuestionsData';
import { PageTitle } from './PageTitle';
import { Page } from './Page';

export const HomePage = () => (
  <Page>
    <div>
      <PageTitle>Unanswered Questions</PageTitle>
      <button>Ask a Question</button>
    </div>
    <QuestionList data={getUnansweredQuestions()} />
  </Page>
);
