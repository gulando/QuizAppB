using System.Collections.Generic;
using System.Linq;

namespace QuizData.Models
{
    public interface IAnswerTypeRepository
    {
        IEnumerable<AnswerType> AnswerTypes { get; }

        AnswerType GetAnswerTypeByID(int answerTypeID);
        
        void AddAnswerType(AnswerType answerType);
        
        void UpdateAnswerType(AnswerType answerType);
        
        AnswerType DeleteAnswerType(int answerTypeID);
        

    }
}