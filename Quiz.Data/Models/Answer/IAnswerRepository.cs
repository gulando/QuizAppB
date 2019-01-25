using System.Collections.Generic;
using System.Linq;


namespace QuizData.Models
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> Answers { get; }

        void UpdateAnswer(Answer answer);
        
        void AddAnswer(Answer answer);
        
        Answer DeleteAnswer(int answerID); 
        
        Answer GetAnswerByID(int answerID);
        
    }
}