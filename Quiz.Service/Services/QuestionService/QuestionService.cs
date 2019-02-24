using System.Collections.Generic;
using System.Linq;
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
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public QuestionService(IRepository<Quiz> quizRepository, IRepository<QuizTheme> quizThemeRepository,
            IRepository<AnswerType> answerTypeRepository,IRepository<QuestionType> questionTypesRepository, 
            IMemoryCache memoryCache, IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
            _quizRepository = quizRepository;
            _quizThemeRepository = quizThemeRepository;
            _answerTypeRepository = answerTypeRepository;
            _questionTypesRepository = questionTypesRepository;
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

        public void UpdateQuestion(Question question)
        {
            _questionRepository.Update(question);
        }

        public void AddQuestion(Question question)
        {
            _questionRepository.Insert(question);
        }

        public void DeleteQuestion(int questionID)
        {
            _questionRepository.Delete(questionID);
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
        
        #endregion
    }
}