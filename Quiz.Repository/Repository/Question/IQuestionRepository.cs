using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> Questions { get; }

        Question GetQuestionByID(int questionID);
        
        void AddQuestion(Question question);
        
        void UpdateQuestion(Question question);
        
        Question DeleteQuestion(int questionID);

        List<QuestionSummary> GetQuestionSummary();


    }
}