using System.Collections.Generic;
using QuizData;


namespace QuizService
{
    public interface IAnswerTypeService
    {
        List<AnswerType> GetAllAnswerTypes();

        AnswerType GetAnswerTypeByID(int answerTypeID);

        void UpdateAnswerType(AnswerType answerType);

        void AddAnswerType(AnswerType answerType);

        void DeleteAnswerType(int answerID);

        List<AnswerTypeSummary> GetAnswerTypeSummary(int answerTypeID = 0);
    }
}