using System.Collections.Generic;
using System.Linq;
using QuizData.Models;
using QuizData.Repository;


namespace QuizData.Models
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }
        
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
    }
}