using System.Linq;


namespace QuizData.Models
{
    public interface IQuestionRepository
    {
        IQueryable<Question> Questions { get; }

        void SaveQuestion(Question question);
        
        Question DeleteQuestion(int questionID);
        
        Question GetQuestionByID(int questionID);
    }
}