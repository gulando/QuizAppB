using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IAnswerService
    {
        List<Answer> GetAllAnswers();

        Answer GetAnswerByID(int answerID);

        void UpdateAnswer(Answer answer);

        void AddAnswer(Answer answer);

        void DeleteAnswer(int answerID);

        List<AnswerSummary> GetAnswerSummary(int answerID = 0);
    }
}