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
        private readonly IRepository<AnswerType> _answerTypeRepository;
        private readonly IRepository<QuizTheme> _quizThemeRepository;
        private readonly IRepository<QuestionType> _questionTypeRepository;
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor
        
        public QuizService(IRepository<Quiz> quizRepository, IRepository<QuizTheme> quizThemeRepository,
            IRepository<QuestionType> questionTypesRepository,
            IRepository<AnswerType> answerTypeRepository,IMemoryCache memoryCache)
        {
            _answerTypeRepository = answerTypeRepository;
            _quizRepository = quizRepository;
            _quizThemeRepository = quizThemeRepository;
            _questionTypeRepository = questionTypesRepository;
            
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
            return _quizRepository.GetById(quizID);
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

        public List<QuizSummary> GetQuizSummary(int quizID, int questionTypeID)
        {
            var result = (from quizzes in _quizRepository.Table
                join quizThemes in _quizThemeRepository.Table on quizzes.ID equals quizThemes.QuizID
                join questionTypes in _questionTypeRepository.Table on quizzes.ID equals questionTypes.QuizID
                join answerTypes in _answerTypeRepository.Table on questionTypes.ID equals answerTypes.QuestionTypeID into tmp
                from answerTypes in tmp.DefaultIfEmpty()
                where (quizzes.ID == quizID || quizID == 0) && (questionTypes.ID == questionTypeID || questionTypeID == 0)
                select new QuizSummary 
                {
                    ID = quizzes.ID,
                    QuizThemeID =  quizThemes.ID,
                    QuestionTypeID = questionTypes.ID,
                    AnswerTypeID = answerTypes == null ? 0 : answerTypes.ID, 
                    QuizName = quizzes.QuizName,
                    QuizThemeName = quizThemes.QuizThemeName,
                    QuestionTypeName = questionTypes.QuestionTypeName,
                    AnswerTypeName = answerTypes == null ? "" : answerTypes.AnswerTypeName
                }).ToList();

            return result;     
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
            return await _quizRepository.GetByIdAsync(quizID);
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

        #region other

        public List<Quiz> GetAllQuizesEF()
        {
            var quizzes = _quizRepository.Table.Include(p => p.QuizThemes).ToList();
            return quizzes;
        }

        #endregion
    }
}