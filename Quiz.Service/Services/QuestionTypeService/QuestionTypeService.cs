using System.Collections.Generic;
using System.Linq;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class QuestionTypeService : IQuestionTypeService
    {
        private readonly IQuestionTypeRepository _questionTypeRepository; 

        public QuestionTypeService(IQuestionTypeRepository questionTypeRepository)
        {
            _questionTypeRepository = questionTypeRepository;
        }

        public IEnumerable<QuestionType> QuestionTypes => _questionTypeRepository.QuestionTypes;
        
        public QuestionType GetQuestionTypeByID(int questionTypeID)
        {
            return _questionTypeRepository.GetQuestionTypeByID(questionTypeID);
        }

        public void AddQuestionType(QuestionType questionType)
        {
            _questionTypeRepository.AddQuestionType(questionType);
        }

        public void UpdateQuestionType(QuestionType questionType)
        {
            _questionTypeRepository.UpdateQuestionType(questionType);
        }

        public QuestionType DeleteQuestionType(int questionTypeID)
        {
            return _questionTypeRepository.DeleteQuestionType(questionTypeID);
        }

        public List<QuestionTypeSummary> GetQuestionTypeSummary(int questionTypeID = 0)
        {
            return _questionTypeRepository.GetQuestionTypeSummary(questionTypeID);
        }
    }
}