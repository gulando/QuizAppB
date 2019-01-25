using System.Collections.Generic;
using System.Linq;


namespace QuizData.Models
{
    public interface IQuestionTypeRepository
    {
        IEnumerable<QuestionType> QuestionTypes { get; }

        QuestionType GetQuestionByID(int questionTypeID);
        
        void AddQuestionType(QuestionType questionType);
        
        void UpdateQuestionType(QuestionType questionType);
        
        QuestionType DeleteQuestionType(int questionTypeID);
        
    }
}