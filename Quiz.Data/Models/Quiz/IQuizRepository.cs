using System.Linq;


namespace QuizData.Models
{
    public interface IQuizRepository
    {
        IQueryable<Quiz> Quizes { get; }

        Quiz GetQuizByID(int quizID);
        
        void AddQuiz(Quiz quiz);
        
        void UpdateQuiz(Quiz quiz);
        
        Quiz DeleteQuiz(int quizID);
    }
}