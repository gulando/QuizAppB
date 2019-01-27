using System.Collections.Generic;
using System.Linq;
using QuizData;
using QuizRepository;

namespace QuizService
{
    public class AnswerTypeService : IAnswerTypeService
    {
        private readonly IAnswerTypeRepository _answerTypeRepository; 

        public AnswerTypeService(IAnswerTypeRepository answerRepository)
        {
            _answerTypeRepository = answerRepository;
        }

        public IEnumerable<AnswerType> AnswerTypes => _answerTypeRepository.AnswerTypes;
        
        public AnswerType GetAnswerTypeByID(int answerTypeID)
        {
            return _answerTypeRepository.GetAnswerTypeByID(answerTypeID);
        }

        public void AddAnswerType(AnswerType answerType)
        {
            _answerTypeRepository.AddAnswerType(answerType);
        }

        public void UpdateAnswerType(AnswerType answerType)
        {
            _answerTypeRepository.UpdateAnswerType(answerType);
        }

        public AnswerType DeleteAnswerType(int answerTypeID)
        {
            return _answerTypeRepository.DeleteAnswerType(answerTypeID);
        }

        public List<AnswerTypeSummary> GetAnswerTypeSummary(int answerTypeID = 0)
        {
            return _answerTypeRepository.GetAnswerTypeSummary().OrderBy(answer => answer.QuizName).ToList();
        }
    }
}