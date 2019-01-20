using System.Linq;
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
            AddQuizTheme(quizTheme);
        }
        
        public void UpdateQuizTheme(QuizTheme quizTheme)
        {
            UpdateQuizTheme(quizTheme);
        }
        
        public QuizTheme DeleteQuizTheme(int quizThemeID)
        {
            return DeleteObj(quizThemeID);
        }
    }
}