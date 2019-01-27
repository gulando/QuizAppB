using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> Answers { get; }
        
        Answer GetAnswerByID(int answerID);

        void AddAnswer(Answer answer);
        
        void UpdateAnswer(Answer answer);
        
        Answer DeleteAnswer(int answerID); 

        List<AnswerSummary> GetAnswerSummary(int answerID = 0);

    }
}