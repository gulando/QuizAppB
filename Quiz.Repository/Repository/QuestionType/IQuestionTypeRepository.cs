using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public interface IQuestionTypeRepository
    {
        IEnumerable<QuestionType> QuestionTypes { get; }

        QuestionType GetQuestionTypeByID(int questionTypeID);
        
        void AddQuestionType(QuestionType questionType);
        
        void UpdateQuestionType(QuestionType questionType);
        
        QuestionType DeleteQuestionType(int questionTypeID);

        List<QuestionTypeSummary> GetQuestionTypeSummary();

    }
}