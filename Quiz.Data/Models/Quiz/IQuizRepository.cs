using System.Linq;


namespace QuizData.Models
{
    public interface IQuizRepository
    {
        IQueryable<Quiz> Quizes { get; }

        void SaveQuiz(Quiz quiz);
        
        Quiz DeleteQuiz(int quizID);
        
        Quiz GetQuizByID(int quizID); 
    }
}