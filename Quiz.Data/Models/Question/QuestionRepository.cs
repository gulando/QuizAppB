using System.Linq;


namespace QuizData.Models
{
    public class QuestionRepository : IQuestionRepository
    {
        private ApplicationDbContext context;
        
        public QuestionRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Question> Questions => context.Questions;
        
        public void SaveQuestion(Question question)
        {
            if (question.QuestionID == 0)
            {
                context.Questions.Add(question);
            }
            else
            {
                var dbEntry = context.Questions.FirstOrDefault(p => p.QuestionID == question.QuestionID);

                if (dbEntry != null)
                {
                    dbEntry.QuestionImage = question.QuestionImage;
                    dbEntry.QuizID = question.QuizID;
                    dbEntry.QuizThemeID = question.QuizThemeID;
                    dbEntry.AnswerTypeID = question.AnswerTypeID;
                    dbEntry.QuestionTypeID = question.QuestionTypeID;
                    dbEntry.CorrectAnswer = question.CorrectAnswer;
                }
            }
            context.SaveChanges();
        }

        public Question DeleteQuestion(int questionID)
        {
            var dbEntry = context.Questions.FirstOrDefault(p => p.QuestionID == questionID);

            if (dbEntry != null)
            {
                context.Questions.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public Question GetQuestionByID(int questionID)
        {
            return context.Questions.Find(questionID);
        }
    }
}