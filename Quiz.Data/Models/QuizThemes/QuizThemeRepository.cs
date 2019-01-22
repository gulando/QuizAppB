using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuizData.Repository;


namespace QuizData.Models
{
    public class QuizThemeRepository : Repository<QuizTheme>, IQuizThemeRepository
    {
        private ApplicationDbContext dbContext;
        
        public QuizThemeRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            dbContext = repositoryContext;
        }

        public IQueryable<QuizTheme> QuizeThemes => GetObjList();

        public QuizTheme GetQuizThemeByID(int quizThemeID)
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

        public List<QuizThemeSummary> GetQuizThemeSummary()
        {
            return dbContext.QuizThemeSummaries.FromSql("GetQuizThemesSummary").ToList();        }
    }
}