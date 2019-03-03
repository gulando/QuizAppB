using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        
        private readonly IRepositoryAsync<Answer> _answerRepositoryAsync;
        private readonly IRepositoryAsync<AnswerType> _answerTypeRepositoryAsync;
        private readonly IRepositoryAsync<Question> _questionRepositoryAsync; 
        
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public AnswerService(IRepository<Answer> answerRepository, IRepository<AnswerType> answerTypeRepository,
            IRepository<Question> questionRepository, IMemoryCache memoryCache,
            IRepositoryAsync<Answer> answerRepositoryAsync, IRepositoryAsync<AnswerType> answerTypeRepositoryAsync,
            IRepositoryAsync<Question> questionRepositoryAsync)
        {
            _answerRepository = answerRepository;
            _answerTypeRepository = answerTypeRepository;
            _questionRepository = questionRepository;
            _answerRepositoryAsync = answerRepositoryAsync;
            _answerTypeRepositoryAsync = answerTypeRepositoryAsync;
            _questionRepositoryAsync = questionRepositoryAsync;
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
        
        public void AddAnswer(Answer answer)
        {
            _memoryCache.Remove(AnswerDefaults.AnswerByIdCacheKey);
            _memoryCache.Remove(AnswerDefaults.AnswerAllCacheKey);
            
            _answerRepository.Insert(answer);
        }
        
        public void UpdateAnswer(Answer answer)
        {
            _memoryCache.Remove(AnswerDefaults.AnswerByIdCacheKey);
            _memoryCache.Remove(AnswerDefaults.AnswerAllCacheKey);
            
            _answerRepository.Update(answer);
        }

        public void DeleteAnswer(int answerID)
        {
            _memoryCache.Remove(AnswerDefaults.AnswerByIdCacheKey);
            _memoryCache.Remove(AnswerDefaults.AnswerAllCacheKey);
            
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
        
        #region async methods
        
        public async Task<List<Answer>> GetAllAnswersAsync()
        {
            if(_memoryCache.TryGetValue(AnswerDefaults.AnswerAllCacheKey, out List<Answer> answers))
                return answers;
            
            answers = await _answerRepositoryAsync.Table.ToListAsync();
            _memoryCache.Set(AnswerDefaults.AnswerAllCacheKey, answers);
            
            return answers;

        }
        
        public async Task<Answer> GetAnswerByIDAsync(int answerID)
        {
            if (_memoryCache.TryGetValue(AnswerDefaults.AnswerByIdCacheKey, out Answer answer)) 
                return answer;
            
            answer = await _answerRepositoryAsync.GetByIdAsync(answerID);
            _memoryCache.Set(AnswerDefaults.AnswerByIdCacheKey, answer);

            return answer;
        }
        
        public async Task AddAnswerAsync(Answer answer)
        {
            _memoryCache.Remove(AnswerDefaults.AnswerByIdCacheKey);
            _memoryCache.Remove(AnswerDefaults.AnswerAllCacheKey);
            
            await _answerRepositoryAsync.InsertAsync(answer);
        }
        
        public async Task UpdateAnswerAsync(Answer answer) 
        {
            _memoryCache.Remove(AnswerDefaults.AnswerByIdCacheKey);
            _memoryCache.Remove(AnswerDefaults.AnswerAllCacheKey);
            
            await _answerRepositoryAsync.UpdateAsync(answer);
        }
        
        public async Task DeleteAnswerAsync(int answerID)
        {
            _memoryCache.Remove(AnswerDefaults.AnswerByIdCacheKey);
            _memoryCache.Remove(AnswerDefaults.AnswerAllCacheKey);
            
            await _answerRepositoryAsync.DeleteAsync(answerID);
        }
        
        public async Task<List<AnswerSummary>> GetAnswerSummaryAsync(int answerID = 0)
        {
            var result = (from answers in _answerRepositoryAsync.Table
                join questions in _questionRepositoryAsync.Table on answers.QuestionID equals questions.ID
                join answerTypes in _answerTypeRepositoryAsync.Table on questions.AnswerTypeID equals answerTypes.ID
                where answers.ID == answerID || answerID == 0
                select new AnswerSummary
                {
                    ID = answers.ID,
                    AnswerTypeID = answerTypes.ID,
                    AnswerText = answers.AnswerText,
                    AnswerTypeName = answerTypes.AnswerTypeName,
                }).ToListAsync();

            return await result;     
            
        }
                
        #endregion
    }
}