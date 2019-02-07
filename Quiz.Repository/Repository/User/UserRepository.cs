using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        protected UserRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<User> Users => GetObjList();

        public User GetUserByID(int id)
        {
            return GetObjByID(id);
        }

        public User Create(User user)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User user)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            DeleteObj(id);
        }
    }
}