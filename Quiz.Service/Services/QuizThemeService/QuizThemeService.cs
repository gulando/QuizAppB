using System.Collections.Generic;
using System.Linq;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class QuizThemeService : IQuizThemeService
    {
        private readonly IQuizThemeRepository _quizThemeRepository; 

        public QuizThemeService(IQuizThemeRepository quizThemeRepository)
        {
            _quizThemeRepository = quizThemeRepository;
        }

        public IEnumerable<QuizTheme> QuizeThemes => _quizThemeRepository.QuizeThemes;
        
        public QuizTheme GetQuizThemeByID(int quizThemeID)
        {
            return _quizThemeRepository.GetQuizThemeByID(quizThemeID);
        }

        public void AddQuizTheme(QuizTheme quizTheme)
        {
            _quizThemeRepository.AddQuizTheme(quizTheme);
        }

        public void UpdateQuizTheme(QuizTheme quizTheme)
        {
            _quizThemeRepository.UpdateQuizTheme(quizTheme);
        }

        public QuizTheme DeleteQuizTheme(int quizThemeID)
        {
            return _quizThemeRepository.DeleteQuizTheme(quizThemeID);
        }

        public List<QuizThemeSummary> GetQuizThemeSummary(int quizThemeID = 0)
        {
            var quizThemeList = _quizThemeRepository.GetQuizThemeSummary(quizThemeID).OrderBy(quizTheme => quizTheme.QuizName).ToList();

            return quizThemeList;
        }
    }
}