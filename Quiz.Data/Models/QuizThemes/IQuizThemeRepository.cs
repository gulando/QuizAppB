using System.Collections.Generic;
using System.Linq;


namespace QuizData.Models
{
    public interface IQuizThemeRepository
    {
        IEnumerable<QuizTheme> QuizeThemes { get; }

        QuizTheme GetQuizThemeByID(int quizThemeID);
        
        void AddQuizTheme(QuizTheme quizTheme);

        void UpdateQuizTheme(QuizTheme quizTheme);
        
        QuizTheme DeleteQuizTheme(int quizThemeID);

        List<QuizThemeSummary> GetQuizThemeSummary();
    }
}