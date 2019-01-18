using System.Linq;
using QuizData.Models;


namespace QuizData.Models
{
    public class QuestionTypeRepository : IQuestionTypeRepository
    {
        private ApplicationDbContext context;
        
        public QuestionTypeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<QuestionType> QuestionTypes => context.QuestionTypes;

        public void SaveQuestionType(QuestionType questionType)
        {
            if (questionType.QuestionTypeID == 0)
            {
                context.QuestionTypes.Add(questionType);
            }
            else
            {
                QuestionType dbEntry = context.QuestionTypes.FirstOrDefault(p => p.QuestionTypeID == questionType.QuestionTypeID);

                if (dbEntry != null)
                {
                    dbEntry.QuestionTypeName = questionType.QuestionTypeName;
                    dbEntry.QuizID = questionType.QuizID;
                }
            }
            context.SaveChanges();
        }

        public QuestionType DeleteQuestionType(int questionTypeID)
        {
            var dbEntry = context.QuestionTypes.FirstOrDefault(p => p.QuestionTypeID == questionTypeID);

            if (dbEntry != null)
            {
                context.QuestionTypes.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public QuestionType GetQuestionByID(int questionTypeID)
        {
            return context.QuestionTypes.Find(questionTypeID);
        }
    }
}