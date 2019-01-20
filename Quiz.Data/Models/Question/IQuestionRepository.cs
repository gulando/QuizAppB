using System.Linq;


namespace QuizData.Models
{
    public interface IQuestionRepository
    {
        IQueryable<Question> Questions { get; }

        Question GetQuestionByID(int questionID);
        
        void AddQuestion(Question question);
        
        void UpdateQuestion(Question question);
        
        Question DeleteQuestion(int questionID);
        
        
    }
}