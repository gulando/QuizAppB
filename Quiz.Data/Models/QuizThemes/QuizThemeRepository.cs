using System.Linq;


namespace QuizData.Models
{
    public class QuizThemeRepository : IQuizThemeRepository
    {
        
        private ApplicationDbContext context;
        
        public QuizThemeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<QuizTheme> QuizeThemes => context.QuizThemes;

        public void SaveQuizTheme(QuizTheme quizTheme)
        {
            if (quizTheme.QuizThemeID == 0)
            {
                context.QuizThemes.Add(quizTheme);
            }
            else
            {
                QuizTheme dbEntry = context.QuizThemes.FirstOrDefault(p => p.QuizThemeID == quizTheme.QuizThemeID);

                if (dbEntry != null)
                {
                    dbEntry.QuizThemeName = quizTheme.QuizThemeName;
                    dbEntry.QuizID = quizTheme.QuizID;
                }
            }
            context.SaveChanges();
        }

        public QuizTheme DeleteQuizTheme(int quizThemeID)
        {
            var dbEntry = context.QuizThemes.FirstOrDefault(p => p.QuizThemeID == quizThemeID);

            if (dbEntry != null)
            {
                context.QuizThemes.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public QuizTheme GetQuizThemeByID(int quizThemeID)
        {
            return context.QuizThemes.Find(quizThemeID);
        }
        
    }
}