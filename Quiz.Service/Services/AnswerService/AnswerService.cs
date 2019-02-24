using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using QuizRepository;
using QuizData;


namespace QuizService
{
    public class AnswerService : IAnswerService
    {
        #region properties
        
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<AnswerType> _answerTypeRepository;
        private readonly IRepository<Question> _questionRepository; 
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor
        
        public AnswerService(IRepository<Answer> answerRepository, IRepository<AnswerType> answerTypeRepository,IRepository<Question> questionRepository, IMemoryCache memoryCache)
        {
            _answerRepository = answerRepository;
            _answerTypeRepository = answerTypeRepository;
            _questionRepository = questionRepository;
            _memoryCache = memoryCache;
        }

        #endregion
        
        #region methods
        
        public List<Answer> GetAllAnswers()
        {
            if (_memoryCache.TryGetValue(AnswerDefaults.AnswerAllCacheKey, out List<Answer> answers)) 
                return answers.ToList();
                
            answers = _answerRepository.Table.ToList();
            _memoryCache.Set(AnswerDefaults.AnswerAllCacheKey, answers);

            return answers.ToList();
        }
        
        public Answer GetAnswerByID(int answerID)
        {
            if (_memoryCache.TryGetValue(AnswerDefaults.AnswerByIdCacheKey, out Answer answer)) 
                return answer;
            
            answer = _answerRepository.GetById(answerID);
            _memoryCache.Set(AnswerDefaults.AnswerByIdCacheKey, answer);

            return answer;
        }
        
        public void UpdateAnswer(Answer answer)
        {
            _answerRepository.Update(answer);
        }

        public void AddAnswer(Answer answer)
        {
            _answerRepository.Insert(answer);
        }

        public void DeleteAnswer(int answerID)
        {
            _answerRepository.Delete(answerID);
        }

        public List<AnswerSummary> GetAnswerSummary(int answerID = 0)
        {
            var result = (from answers in _answerRepository.Table
                join questions in _questionRepository.Table on answers.QuestionID equals questions.ID
                join answerTypes in _answerTypeRepository.Table on questions.AnswerTypeID equals answerTypes.ID
                where answers.ID == answerID || answerID == 0
                select new AnswerSummary
                {
                    ID = answers.ID,
                    AnswerTypeID = answerTypes.ID,
                    AnswerText = answers.AnswerText,
                    AnswerTypeName = answerTypes.AnswerTypeName,
                }).ToList();

            return result;     
            
        }

        #endregion
    }
}