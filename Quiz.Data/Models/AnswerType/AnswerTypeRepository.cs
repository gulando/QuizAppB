using System.Collections.Generic;
using System.Linq;
using QuizData.Repository;


namespace QuizData.Models
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
    }
}