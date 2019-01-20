using System.Linq;
using QuizData.Repository;


namespace QuizData.Models
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IQueryable<Question> Questions => GetObjList();
        
        public Question GetQuestionByID(int questionID)
        {
            return GetObjByID(questionID);
        }

        public void AddQuestion(Question question)
        {
            AddObj(question);
        }

        public void UpdateQuestion(Question question)
        {
            UpdateObj(question);
        }

        public Question DeleteQuestion(int questionID)
        {
            return DeleteObj(questionID);
        }
    }
}