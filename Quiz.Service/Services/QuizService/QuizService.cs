using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class QuizService : IQuizService
    {
        #region properties
        
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor
        
        public QuizService(IRepository<Quiz> quizRepository,IMemoryCache memoryCache)
        {
            _quizRepository = quizRepository;
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods 
        
        public List<Quiz> GetAllQuizes()
        {
            if (_memoryCache.TryGetValue(QuizDefaults.QuizAllCacheKey, out List<Quiz> quizzes)) 
                return quizzes.ToList();
                
            quizzes = _quizRepository.Table.ToList();
            _memoryCache.Set(QuizDefaults.QuizAllCacheKey, quizzes);

            return quizzes.ToList();
        }

        public Quiz GetQuizByID(int quizID)
        {
            if (_memoryCache.TryGetValue(QuizDefaults.QuizIdCacheKey, out Quiz quiz)) 
                return quiz;
            
            quiz = _quizRepository.GetById(quizID);
            _memoryCache.Set(QuizDefaults.QuizIdCacheKey, quiz);

            return quiz;
        }

        public void UpdateQuiz(Quiz quiz)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            _quizRepository.Update(quiz);
        }

        public void AddQuiz(Quiz quiz)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            _quizRepository.Insert(quiz);
        }

        public void DeleteQuiz(int quizID)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            _quizRepository.Delete(quizID);
        }

        #endregion
        
        #region async methods
        
        public async Task<List<Quiz>> GetAllQuizesAsync()
        {
            if (_memoryCache.TryGetValue(QuizDefaults.QuizAllCacheKey, out List<Quiz> quizzes)) 
                return quizzes.ToList();
                
            quizzes = await _quizRepository.Table.ToListAsync();
            _memoryCache.Set(QuizDefaults.QuizAllCacheKey, quizzes);

            return quizzes.ToList();
        }

        public async Task<Quiz> GetQuizByIDAsync(int quizID)
        {
            if (_memoryCache.TryGetValue(QuizDefaults.QuizIdCacheKey, out Quiz quiz)) 
                return quiz;
            
            quiz = await _quizRepository.GetByIdAsync(quizID);
            _memoryCache.Set(QuizDefaults.QuizIdCacheKey, quiz);

            return quiz;
        }

        public async Task AddQuizAsync(Quiz quiz)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            await _quizRepository.InsertAsync(quiz);
        }

        public async Task UpdateQuizAsync(Quiz quiz)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            await _quizRepository.UpdateAsync(quiz);
        }

        public async Task DeleteQuizAsync(int quizID)
        {
            _memoryCache.Remove(QuizDefaults.QuizAllCacheKey);
            _memoryCache.Remove(QuizDefaults.QuizIdCacheKey);
            
            await _quizRepository.DeleteAsync(quizID);
        }

        #endregion
    }
}