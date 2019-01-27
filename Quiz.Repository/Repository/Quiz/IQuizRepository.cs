using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public interface IQuizRepository
    {
        IEnumerable<Quiz> Quizes { get; }

        Quiz GetQuizByID(int quizID);
        
        void AddQuiz(Quiz quiz);
        
        void UpdateQuiz(Quiz quiz);
        
        Quiz DeleteQuiz(int quizID);
    }
}