using System.Collections.Generic;
using System.Linq;
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
        
        public QuizService(IRepository<Quiz> quizRepository, IMemoryCache memoryCache)
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
            _quizRepository.Update(quiz);
        }

        public void AddQuiz(Quiz quiz)
        {
            _quizRepository.Insert(quiz);
        }

        public void DeleteQuiz(int quizID)
        {
            _quizRepository.Delete(quizID);
        }
        
        #endregion
    }
}