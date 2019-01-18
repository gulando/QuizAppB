using System.Linq;
using QuizData.Models;


namespace QuizData.Models
{
    public class AnswerRepository : IAnswerRepository
    {
        private ApplicationDbContext context;
        
        public AnswerRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Answer> Answers => context.Answers;
        
        public void SaveAnswer(Answer answer)
        {
            if (answer.AnswerID == 0)
            {
                context.Answers.Add(answer);
            }
            else
            {
                var dbEntry = context.Answers.FirstOrDefault(p => p.AnswerID == answer.AnswerID);

                if (dbEntry != null)
                {
                    dbEntry.AnswerText = answer.AnswerText;
                    dbEntry.QuestionID = answer.QuestionID;
                    dbEntry.AnswerTypeID = answer.AnswerTypeID;
                }
            }
            context.SaveChanges();
        }

        public Answer DeleteAnswer(int answerID)
        {
            var dbEntry = context.Answers.FirstOrDefault(p => p.AnswerID == answerID);

            if (dbEntry != null)
            {
                context.Answers.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public Answer GetAnswerByID(int answerID)
        {
            return context.Answers.Find(answerID);
        }
    }
}