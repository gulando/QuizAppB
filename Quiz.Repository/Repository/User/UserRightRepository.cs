using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public class UserRightRepository : Repository<UserRight>, IUserRightRepository
    {
        public UserRightRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<UserRight> UserRights => GetObjList();
        
        public UserRight GetUserRightByID(int id)
        {
            return GetObjByID(id);
        }

        public UserRight Create(UserRight userRight)
        {
            return AddObj(userRight);
        }

        public void Update(UserRight userRight)
        {
            UpdateObj(userRight);
        }

        public void DeleteUserRight(int id)
        {
            DeleteObj(id);
        }
    }
}