using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class QuizThemeService : IQuizThemeService
    {
        #region properties
        
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<QuizTheme> _quizThemeRepository;
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public QuizThemeService(IRepository<Quiz> quizRepository, IRepository<QuizTheme> quizThemeRepository,
            IMemoryCache memoryCache)
        {
            _quizRepository = quizRepository;
            _quizThemeRepository = quizThemeRepository;
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods
        
        public List<QuizTheme> GetAllQuizThemes()
        {
            if (_memoryCache.TryGetValue(QuizThemeDefaults.QuizThemeAllCacheKey, out List<QuizTheme> quizThemes)) 
                return quizThemes.ToList();
                
            quizThemes = _quizThemeRepository.Table.ToList();
            _memoryCache.Set(QuizThemeDefaults.QuizThemeAllCacheKey, quizThemes);

            return quizThemes.ToList();
        }

        public QuizTheme GetQuizThemeByID(int quizThemeID)
        {
            if (_memoryCache.TryGetValue(QuizDefaults.QuizIdCacheKey, out QuizTheme quizTheme)) 
                return quizTheme;
            
            quizTheme = _quizThemeRepository.GetById(quizThemeID);
            _memoryCache.Set(QuizDefaults.QuizIdCacheKey, quizTheme);

            return quizTheme;
        }

        public void UpdateQuizTheme(QuizTheme quizTheme)
        {
            _quizThemeRepository.Update(quizTheme);
        }

        public void AddQuizTheme(QuizTheme quizTheme)
        {
            _quizThemeRepository.Insert(quizTheme);
        }

        public void DeleteQuizTheme(int quizThemeID)
        {
            _quizThemeRepository.Delete(quizThemeID);
        }

        public List<QuizThemeSummary> GetQuizThemeSummary(int quizThemeID = 0)
        {
            var result = (from quizes in _quizRepository.Table
                join quizThemes in _quizThemeRepository.Table on quizes.ID equals quizThemes.QuizID
                where quizThemes.ID == quizThemeID || quizThemeID == 0
                select new QuizThemeSummary
                {
                    QuizID = quizes.ID,
                    ID = quizThemes.ID,
                    QuizName = quizes.QuizName,
                    QuizThemeName = quizThemes.QuizThemeName
                }).ToList();

            return result;     
        }
        
        #endregion
    }
}