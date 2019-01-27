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
                join quizes in dbContext.Quizes on questions.QuizID equals quizes.ID
                join quizThemes in dbContext.QuizThemes on questions.QuizThemeID equals quizThemes.ID
                where answers.ID == answerID || answers.ID == 0
                select new AnswerSummary 
                {
                    ID = answers.ID,
                    AnswerTypeID = answerTypes.ID,
                    AnswerTypeName = answerTypes.AnswerTypeName,
                }).ToList();

            return result;     
        }
        
        
        #endregion
    }
}