using System.Linq;


namespace QuizData.Models
{
    public interface IQuizThemeRepository
    {
        IQueryable<QuizTheme> QuizeThemes { get; }

        void SaveQuizTheme(QuizTheme quizTheme);
        
        QuizTheme DeleteQuizTheme(int quizThemeID);
        
        QuizTheme GetQuizThemeByID(int quizThemeID);
    }
}