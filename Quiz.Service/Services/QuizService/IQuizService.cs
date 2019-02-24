using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IQuizService
    {
        List<Quiz> GetAllQuizes();

        Quiz GetQuizByID(int quizID);

        void UpdateQuiz(Quiz quiz);

        void AddQuiz(Quiz quiz);

        void DeleteQuiz(int quizID);
    }
}