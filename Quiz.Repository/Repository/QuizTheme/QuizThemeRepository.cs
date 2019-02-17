using System.Collections.Generic;
using System.Linq;
using QuizData;


namespace QuizRepository
{
    public class QuizThemeRepository : Repository<QuizTheme>, IQuizThemeRepository
    {        
        public QuizThemeRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<QuizTheme> QuizeThemes => GetObjList();

        public QuizData.QuizTheme GetQuizThemeByID(int quizThemeID)
        {
            return GetObjByID(quizThemeID);
        }
        
        public void AddQuizTheme(QuizTheme quizTheme)
        {
            AddObj(quizTheme);
        }
        
        public void UpdateQuizTheme(QuizTheme quizTheme)
        {
            UpdateObj(quizTheme);
        }
        
        public QuizTheme DeleteQuizTheme(int quizThemeID)
        {
            return DeleteObj(quizThemeID);
        }

        public List<QuizThemeSummary> GetQuizThemeSummary(int quizThemeID = 0)
        {
            var result = (from quizes in dbContext.Quizes
                join quizThemes in dbContext.QuizThemes on quizes.ID equals quizThemes.QuizID
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
    }
}