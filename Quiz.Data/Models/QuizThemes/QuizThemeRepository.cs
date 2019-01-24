using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using QuizData.Repository;


namespace QuizData.Models
{
    public class QuizThemeRepository : Repository<QuizTheme>, IQuizThemeRepository
    {        
        public QuizThemeRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
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
            var result = (from quizes in dbContext.Quizes
                join quizThemes in dbContext.QuizThemes on quizes.ID equals quizThemes.QuizID
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