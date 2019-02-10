using System;
using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<User> Users => GetObjList();

        public User GetUserByID(int id)
        {
            return GetObjByID(id);
        }

        public User Create(User user)
        {
            return AddObj(user);
        }

        public void Update(User user)
        {
            UpdateObj(user);
        }

        public void DeleteUser(int id)
        {
            DeleteObj(id);
        }
    }
}