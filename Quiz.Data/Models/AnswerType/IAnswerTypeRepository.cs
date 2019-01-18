using System.Linq;

namespace QuizData.Models
{
    public interface IAnswerTypeRepository
    {
        IQueryable<AnswerType> AnswerTypes { get; }

        void SaveAnswerType(AnswerType answerType);
        
        AnswerType DeleteAnswerType(int answerTypeID);
        
        AnswerType GetAnswerTypeByID(int answerTypeID);
    }
}