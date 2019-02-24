using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class QuestionTypeService : IQuestionTypeService
    {
        #region properties
        
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<QuestionType> _questionTypesRepository; 
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public QuestionTypeService(IRepository<Quiz> quizRepository, IRepository<QuestionType> questionTypesRepository, 
            IMemoryCache memoryCache)
        {
            _quizRepository = quizRepository;
            _questionTypesRepository = questionTypesRepository;
            _memoryCache = memoryCache;
        }

        #endregion


        #region methods
        
        public List<QuestionType> GetAllQuestionTypes()
        {
            if (_memoryCache.TryGetValue(QuestionTypeDefaults.QuestionTypeAllCacheKey, out List<QuestionType> questionTypes))
                return questionTypes;

            questionTypes = _questionTypesRepository.Table.ToList();
            _memoryCache.Set(QuestionDefaults.QuestionAllCacheKey, questionTypes);
    
            return questionTypes;
        }

        public QuestionType GetQuestionTypeByID(int questionTypeID)
        {
            if (_memoryCache.TryGetValue(QuestionDefaults.QuestionyIdCacheKey, out QuestionType questionType)) 
                return questionType;

            questionType = _questionTypesRepository.GetById(questionTypeID);
            _memoryCache.Set(QuestionDefaults.QuestionyIdCacheKey, questionType);

            return questionType;
        }

        public void UpdateQuestionType(QuestionType questionType)
        {
           _questionTypesRepository.Update(questionType);
        }

        public void AddQuestionType(QuestionType questionType)
        {
            _questionTypesRepository.Insert(questionType);
        }

        public void DeleteQuestionType(int questionTypeID)
        {
            _questionTypesRepository.Delete(questionTypeID);
        }

        public List<QuestionTypeSummary> GetQuestionTypeSummary(int questionTypeID = 0)
        {
            var result = (from questionTypes in _questionTypesRepository.Table
                join quizes in _quizRepository.Table on questionTypes.QuizID equals quizes.ID
                orderby quizes.QuizName
                where questionTypes.ID == questionTypeID || questionTypeID == 0
                select new QuestionTypeSummary
                {
                    ID = questionTypes.ID,
                    QuizID = quizes.ID,
                    QuizName = quizes.QuizName,
                    QuestionTypeName = questionTypes.QuestionTypeName
                }).ToList();

            return result;     
        }
        
        #endregion
    }
}