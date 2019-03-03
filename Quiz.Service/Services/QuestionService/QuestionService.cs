using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class QuestionService : IQuestionService
    {
        #region properties
        
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Quiz> _quizRepository;
        private readonly IRepository<QuizTheme> _quizThemeRepository;
        private readonly IRepository<AnswerType> _answerTypeRepository;
        private readonly IRepository<QuestionType> _questionTypesRepository;
        
        private readonly IRepositoryAsync<Question> _questionRepositoryAsync;
        private readonly IRepositoryAsync<Quiz> _quizRepositoryAsync;
        private readonly IRepositoryAsync<QuizTheme> _quizThemeRepositoryAsync;
        private readonly IRepositoryAsync<AnswerType> _answerTypeRepositoryAsync;
        private readonly IRepositoryAsync<QuestionType> _questionTypesRepositoryAsync; 
        
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public QuestionService(IRepository<Quiz> quizRepository, IRepository<QuizTheme> quizThemeRepository,
            IRepository<AnswerType> answerTypeRepository, IRepository<QuestionType> questionTypesRepository,
            IRepository<Question> questionRepository,IRepositoryAsync<Question> questionRepositoryAsync, 
            IRepositoryAsync<Quiz> quizRepositoryAsync,
            IRepositoryAsync<QuizTheme> quizThemeRepositoryAsync,
            IRepositoryAsync<AnswerType> answerTypeRepositoryAsync,
            IRepositoryAsync<QuestionType> questionTypesRepositoryAsync,
            IMemoryCache memoryCache)
        {
            _questionRepository = questionRepository;
            _quizRepository = quizRepository;
            _quizThemeRepository = quizThemeRepository;
            _answerTypeRepository = answerTypeRepository;
            _questionTypesRepository = questionTypesRepository;
            
            _questionRepositoryAsync = questionRepositoryAsync;
            _quizRepositoryAsync = quizRepositoryAsync;
            _quizThemeRepositoryAsync = quizThemeRepositoryAsync;
            _answerTypeRepositoryAsync = answerTypeRepositoryAsync;
            _questionTypesRepositoryAsync = questionTypesRepositoryAsync;
            
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods
        
        public List<Question> GetAllQuestions()
        {
            if (_memoryCache.TryGetValue(QuestionDefaults.QuestionAllCacheKey, out List<Question> questions))
                return questions;

            questions = _questionRepository.Table.ToList();
            _memoryCache.Set(QuestionDefaults.QuestionAllCacheKey, questions);
    
            return questions;
        }

        public Question GetQuestionByID(int questionID)
        {
            if (_memoryCache.TryGetValue(QuestionDefaults.QuestionyIdCacheKey, out Question question)) 
                return question;

            question = _questionRepository.GetById(questionID);
            _memoryCache.Set(QuestionDefaults.QuestionyIdCacheKey, question);

            return question;
        }

        public List<QuestionSummary> GetQuestionSummary(int questionID = 0)
        {
            var result = (from questions in _questionRepository.Table
                join quizes in _quizRepository.Table on questions.QuizID equals quizes.ID
                join quizThemes in _quizThemeRepository.Table on questions.QuizThemeID equals quizThemes.ID
                join answerTypes in _answerTypeRepository.Table on questions.AnswerTypeID equals answerTypes.ID
                join questionTypes in _questionTypesRepository.Table on questions.QuestionTypeID equals questionTypes.ID
                where questions.ID == questionID || questionID == 0
                select new QuestionSummary 
                {
                    ID = questions.ID,
                    QuizID = quizes.ID,
                    QuizThemeID =  quizThemes.ID,
                    QuestionTypeID = questionTypes.ID,
                    AnswerTypeID = answerTypes.ID,
                    QuizName = quizes.QuizName,
                    QuizThemeName = quizThemes.QuizThemeName,
                    QuestionTypeName = questionTypes.QuestionTypeName,
                    AnswerTypeName = answerTypes.AnswerTypeName,
                    CorrectAnswer = questions.CorrectAnswer
                }).ToList();

            return result;     
        }
        
        public void UpdateQuestion(Question question)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            _questionRepository.Update(question);
        }

        public void AddQuestion(Question question)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            _questionRepository.Insert(question);
        }

        public void DeleteQuestion(int questionID)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            _questionRepository.Delete(questionID);
        }

        #endregion
        
        #region async methods
        
        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            if (_memoryCache.TryGetValue(QuestionDefaults.QuestionAllCacheKey, out List<Question> questions))
                return questions;

            questions = await _questionRepositoryAsync.Table.ToListAsync();
            _memoryCache.Set(QuestionDefaults.QuestionAllCacheKey, questions);
    
            return questions;
        }

        public async Task<List<QuestionSummary>> GetQuestionSummaryAsync(int questionID = 0)
        {
            var result = (from questions in _questionRepositoryAsync.Table
                join quizes in _quizRepositoryAsync.Table on questions.QuizID equals quizes.ID
                join quizThemes in _quizThemeRepositoryAsync.Table on questions.QuizThemeID equals quizThemes.ID
                join answerTypes in _answerTypeRepositoryAsync.Table on questions.AnswerTypeID equals answerTypes.ID
                join questionTypes in _questionTypesRepositoryAsync.Table on questions.QuestionTypeID equals questionTypes.ID
                where questions.ID == questionID || questionID == 0
                select new QuestionSummary 
                {
                    ID = questions.ID,
                    QuizID = quizes.ID,
                    QuizThemeID =  quizThemes.ID,
                    QuestionTypeID = questionTypes.ID,
                    AnswerTypeID = answerTypes.ID,
                    QuizName = quizes.QuizName,
                    QuizThemeName = quizThemes.QuizThemeName,
                    QuestionTypeName = questionTypes.QuestionTypeName,
                    AnswerTypeName = answerTypes.AnswerTypeName,
                    CorrectAnswer = questions.CorrectAnswer
                }).ToListAsync();

            return await result;    
        }

        public async Task<Question> GetQuestionByIDAsync(int questionID)
        {
            if (_memoryCache.TryGetValue(QuestionDefaults.QuestionyIdCacheKey, out Question question)) 
                return question;

            question = await _questionRepositoryAsync.GetByIdAsync(questionID);
            _memoryCache.Set(QuestionDefaults.QuestionyIdCacheKey, question);

            return question;
        }

        public async Task AddQuestionAsync(Question question)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            await _questionRepositoryAsync.InsertAsync(question);
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            await _questionRepositoryAsync.UpdateAsync(question);
        }

        public async Task DeleteQuestionAsync(int questionID)
        {
            _memoryCache.Remove(QuestionDefaults.QuestionAllCacheKey);
            _memoryCache.Remove(QuestionDefaults.QuestionyIdCacheKey);
            
            await _questionRepositoryAsync.DeleteAsync(questionID);
        }
        
        #endregion
    }
}