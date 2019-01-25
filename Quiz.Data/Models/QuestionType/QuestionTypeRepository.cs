using System.Collections.Generic;
using System.Linq;
using QuizData.Repository;


namespace QuizData.Models
{
    public class QuestionTypeRepository : Repository<QuestionType>, IQuestionTypeRepository
    {
        public QuestionTypeRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<QuestionType> QuestionTypes => GetObjList();

        public QuestionType GetQuestionByID(int questionTypeID)
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
    }
}