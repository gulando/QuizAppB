using System.Linq;


namespace QuizData.Models
{
    public interface IQuestionTypeRepository
    {
        IQueryable<QuestionType> QuestionTypes { get; }

        void SaveQuestionType(QuestionType questionType);
        
        QuestionType DeleteQuestionType(int questionTypeID);
        
        QuestionType GetQuestionByID(int questionTypeID);
    }
}