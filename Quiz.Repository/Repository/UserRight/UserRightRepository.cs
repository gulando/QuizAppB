using System.Collections.Generic;
using System.Linq;
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
        
        public List<UserRightSummary> GetUserRightSummary()
        {
            var result = (from userRights in dbContext.UserRights
                join users in dbContext.Users on userRights.UserID equals users.ID
                join rights in dbContext.Rights on userRights.RightID equals rights.ID
                select new UserRightSummary
                {
                    ID = userRights.ID,
                    UserID = userRights.UserID,
                    RightID = userRights.RightID,
                    UserName = users.Username,
                    RightName = rights.Name
                }).ToList();

            return result;     
        }
    }
}