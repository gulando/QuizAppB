using System.Linq;


namespace QuizData.Models
{
    public interface IAnswerRepository
    {
        IQueryable<Answer> Answers { get; }

        void UpdateAnswer(Answer answer);
        
        void AddAnswer(Answer answer);
        
        Answer DeleteAnswer(int answerID); 
        
        Answer GetAnswerByID(int answerID);
        
    }
}