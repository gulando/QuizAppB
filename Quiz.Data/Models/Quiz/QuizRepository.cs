using System.Linq;


namespace QuizData.Models
{
    public class QuizRepository : IQuizRepository
    {
        private ApplicationDbContext context;
        
        public QuizRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Quiz> Quizes => context.Quizes;
        
        public void SaveQuiz(Quiz quiz)
        {
            if (quiz.QuizID == 0)
            {
                context.Quizes.Add(quiz);
            }
            else
            {
                Quiz dbEntry = context.Quizes.FirstOrDefault(p => p.QuizID == quiz.QuizID);

                if (dbEntry != null)
                {
                    dbEntry.QuizName = quiz.QuizName;
                }
            }
            context.SaveChanges();
        }

        public Quiz DeleteQuiz(int quizID)
        {
            var dbEntry = context.Quizes.FirstOrDefault(p => p.QuizID == quizID);

            if (dbEntry != null)
            {
                context.Quizes.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public Quiz GetQuizByID(int quizID)
        {
            return context.Quizes.Find(quizID);
        }
    }
}