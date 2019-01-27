using System.Collections.Generic;
using QuizData;
using System.Linq;


namespace QuizRepository
{
    public class AnswerTypeRepository : Repository<AnswerType>,  IAnswerTypeRepository
    {
        public AnswerTypeRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<AnswerType> AnswerTypes => GetObjList();
        
        public AnswerType GetAnswerTypeByID(int answerTypeID)
        {
            return GetObjByID(answerTypeID);
        }

        public void AddAnswerType(AnswerType answerType)
        {
            AddObj(answerType);
        }

        public void UpdateAnswerType(AnswerType answerType)
        {
            UpdateObj(answerType);
        }

        public AnswerType DeleteAnswerType(int answerTypeID)
        {
            return DeleteObj(answerTypeID);
        }
        
        public List<AnswerTypeSummary> GetAnswerTypeSummary(int answerTypeID = 0)
        {
            var result = (from answerTypes in dbContext.AnswerTypes
                join quizes in dbContext.Quizes on answerTypes.QuizID equals quizes.ID
                join questionTypes in dbContext.QuestionTypes on answerTypes.QuestionTypeID equals questionTypes.ID
                where answerTypes.ID == answerTypeID || answerTypeID == 0
                select new AnswerTypeSummary()
                {
                    ID = answerTypes.ID,
                    QuizID = quizes.ID,
                    QuestionTypeID = questionTypes.ID,
                    QuizName = quizes.QuizName,
                    QuestionTypeName = questionTypes.QuestionTypeName,
                    AnswerTypeName = answerTypes.AnswerTypeName
                }).ToList();

            return result;     
        }
    }
}