using System.Collections.Generic;
using QuizData;


namespace QuizService
{
    public interface IQuestionService
    {
        List<Question> GetAllQuestions();

        Question GetQuestionByID(int questionID);

        void UpdateQuestion(Question question);

        void AddQuestion(Question question);

        void DeleteQuestion(int questionID);

        List<QuestionSummary> GetQuestionSummary(int questionID = 0);
    }
}