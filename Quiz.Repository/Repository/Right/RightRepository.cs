using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public class RightRepository : Repository<Right>, IRightRepository
    {
        public RightRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<Right> Rights => GetObjList();
        
        public Right GetRightByID(int id)
        {
            return GetObjByID(id);
        }

        public Right Create(Right right)
        {
            return AddObj(right);
        }

        public void Update(Right right)
        {
            UpdateObj(right);
        }

        public void DeleteRight(int id)
        {
            DeleteObj(id);
        }
    }
}