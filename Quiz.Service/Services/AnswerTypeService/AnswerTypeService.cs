using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class AnswerTypeService : IAnswerTypeService
    {
        #region properties
        
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<AnswerType> _answerTypeRepository;
        private readonly IRepository<QuestionType> _questionTypesRepository; 
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor
        
        public AnswerTypeService(IRepository<Quiz> quizRepository, IRepository<AnswerType> answerTypeRepository,IRepository<QuestionType> questionTypesRepository, IMemoryCache memoryCache)
        {
            _quizRepository = quizRepository;
            _answerTypeRepository = answerTypeRepository;
            _questionTypesRepository = questionTypesRepository;
            _memoryCache = memoryCache;
        }

        #endregion
        
        #region methods
        
        public List<AnswerType> GetAllAnswerTypes()
        {
            if (_memoryCache.TryGetValue(AnswerTypeDefaults.AnswerTypeAllCacheKey, out List<AnswerType> answerTypes))
                return answerTypes;

            answerTypes = _answerTypeRepository.Table.ToList();
            _memoryCache.Set(AnswerTypeDefaults.AnswerTypeAllCacheKey, answerTypes);
    
            return answerTypes;
            
        }
        
        public AnswerType GetAnswerTypeByID(int answerTypeID)
        {
            if (_memoryCache.TryGetValue(AnswerTypeDefaults.AnswerTypeByIdCacheKey, out AnswerType answerType)) 
                return answerType;

            answerType = _answerTypeRepository.GetById(answerTypeID);
            _memoryCache.Set(AnswerTypeDefaults.AnswerTypeByIdCacheKey, answerType);

            return answerType;
        }

        public void AddAnswerType(AnswerType answerType)
        {
            _answerTypeRepository.Insert(answerType);
        }

        public void UpdateAnswerType(AnswerType answerType)
        {
            _answerTypeRepository.Update(answerType);
        }

        public void DeleteAnswerType(int answerTypeID)
        {
            _answerTypeRepository.Delete(answerTypeID);
        }

        public List<AnswerTypeSummary> GetAnswerTypeSummary(int answerTypeID = 0)
        {
            var result = (from answerTypes in _answerTypeRepository.Table
                join quizes in _quizRepository.Table on answerTypes.QuizID equals quizes.ID
                join questionTypes in _questionTypesRepository.Table on answerTypes.QuestionTypeID equals questionTypes.ID
                where answerTypes.ID == answerTypeID || answerTypeID == 0
                select new AnswerTypeSummary()
                {
                    ID = answerTypes.ID,
                    QuizID = quizes.ID,
                    QuestionTypeID = questionTypes.ID,
                    QuizName = quizes.QuizName,
                    QuestionTypeName = questionTypes.QuestionTypeName,
                    AnswerTypeName = answerTypes.AnswerTypeName
                }).ToList();

            return result;     
        }

        #endregion
    }
}