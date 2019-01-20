using System.Linq;
using QuizData.Repository;


namespace QuizData.Models
{
    public class QuizRepository : Repository<Quiz>, IQuizRepository
    {
        public QuizRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IQueryable<Quiz> Quizes => GetObjList();

        public Quiz GetQuizByID(int quizID)
        {
            return GetObjByID(quizID);
        }
        
        public void AddQuiz(Quiz quiz)
        {
            AddObj(quiz);
        }
        
        public void UpdateQuiz(Quiz quiz)
        {
            UpdateObj(quiz);
        }

        public Quiz DeleteQuiz(int quizID)
        {
            return DeleteObj(quizID);
        }
    }
}