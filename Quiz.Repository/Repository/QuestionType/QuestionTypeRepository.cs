using System.Collections.Generic;
using System.Linq;
using QuizData;


namespace QuizRepository
{
    public class QuestionTypeRepository : Repository<QuestionType>, IQuestionTypeRepository
    {
        public QuestionTypeRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<QuestionType> QuestionTypes => GetObjList();

        public QuestionType GetQuestionTypeByID(int questionTypeID)
        {
            return GetObjByID(questionTypeID);
        }

        public void AddQuestionType(QuestionType questionType)
        {
            AddObj(questionType);
        }

        public void UpdateQuestionType(QuestionType questionType)
        {
            UpdateObj(questionType);
        }

        public QuestionType DeleteQuestionType(int questionTypeID)
        {
            return DeleteObj(questionTypeID);
        }
        
        public List<QuestionTypeSummary> GetQuestionTypeSummary(int questionTypeID = 0)
        {
            var result = (from questionTypes in dbContext.QuestionTypes
                join quizes in dbContext.Quizes on questionTypes.QuizID equals quizes.ID
                orderby quizes.QuizName
                where questionTypes.ID == questionTypeID || questionTypeID == 0
                select new QuestionTypeSummary
                {
                    ID = questionTypes.ID,
                    QuizID = quizes.ID,
                    QuizName = quizes.QuizName,
                    QuestionTypeName = questionTypes.QuestionTypeName
                }).ToList();

            return result;     
        }
    }
}