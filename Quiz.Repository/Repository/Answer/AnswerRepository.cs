using System.Collections.Generic;
using QuizData;
using System.Linq;


namespace QuizRepository
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        #region ctor
        
        public AnswerRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }
        
        #endregion
        
        #region crud
        
        public IEnumerable<Answer> Answers => GetObjList();

        public Answer GetAnswerByID(int answerID)
        {
            return GetObjByID(answerID);
        }
        
        public void AddAnswer(Answer answer)
        {
            AddObj(answer);
        }
        
        public void UpdateAnswer(Answer answer)
        {
            UpdateObj(answer);
        }

        public Answer DeleteAnswer(int answerID)
        {
            return DeleteObj(answerID);
        }
        
        #endregion
              
        #region other
        
        public List<AnswerSummary> GetAnswerSummary(int answerID = 0)
        {
            var result = (from answers in dbContext.Answers
                join questions in dbContext.Questions on answers.QuestionID equals questions.ID
                join answerTypes in dbContext.AnswerTypes on questions.AnswerTypeID equals answerTypes.ID
                where answers.ID == answerID || answerID == 0
                select new AnswerSummary
                {
                    ID = answers.ID,
                    AnswerTypeID = answerTypes.ID,
                    AnswerText = answers.AnswerText,
                    AnswerTypeName = answerTypes.AnswerTypeName,
                }).ToList();

            return result;     
        }
        
        
        #endregion
    }
}