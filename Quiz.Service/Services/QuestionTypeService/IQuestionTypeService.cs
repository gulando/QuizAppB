using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IQuestionTypeService
    {
        List<QuestionType> GetAllQuestionTypes();

        QuestionType GetQuestionTypeByID(int questionTypeID);

        void UpdateQuestionType(QuestionType questionType);

        void AddQuestionType(QuestionType questionType);

        void DeleteQuestionType(int questionTypeID);

        List<QuestionTypeSummary> GetQuestionTypeSummary(int questionTypeID = 0);
    }
}