using System.Linq;


namespace QuizData.Models
{
    public interface IAnswerRepository
    {
        IQueryable<Answer> Answers { get; }

        void SaveAnswer(Answer answer);
        
        Answer DeleteAnswer(int answerID); 
        
        Answer GetAnswerByID(int answerID);
        
    }
}