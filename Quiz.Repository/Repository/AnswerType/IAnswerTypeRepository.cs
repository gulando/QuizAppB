using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public interface IAnswerTypeRepository
    {
        IEnumerable<AnswerType> AnswerTypes { get; }

        AnswerType GetAnswerTypeByID(int answerTypeID);
        
        void AddAnswerType(AnswerType answerType);
        
        void UpdateAnswerType(AnswerType answerType);
        
        AnswerType DeleteAnswerType(int answerTypeID);

        List<AnswerTypeSummary> GetAnswerTypeSummary(int answerTypeID = 0);

    }
}