using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IQuizThemeService
    {
        List<QuizTheme> GetAllQuizThemes();

        QuizTheme GetQuizThemeByID(int quizThemeID);

        void UpdateQuizTheme(QuizTheme quizTheme);

        void AddQuizTheme(QuizTheme quizTheme);

        void DeleteQuizTheme(int quizThemeID);

        List<QuizThemeSummary> GetQuizThemeSummary(int quizThemeID = 0);
    }
}