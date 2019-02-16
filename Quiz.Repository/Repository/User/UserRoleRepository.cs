using System.Collections.Generic;
using System.Linq;
using QuizData;


namespace QuizRepository
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<UserRole> UserRoles => GetObjList();
        
        public UserRole GetUserRoleByID(int id)
        {
            return GetObjByID(id);
        }

        public UserRole Create(UserRole userRole)
        {
            return AddObj(userRole);
        }

        public void Update(UserRole userRole)
        {
            UpdateObj(userRole);
        }

        public void DeleteUserRole(int id)
        {
            DeleteObj(id);
        }

        public List<UserRoleSummary> GetUserRoleDataList()
        {
            var result = (from userRoles in dbContext.UserRoles
                join users in dbContext.Users on userRoles.UserID equals users.ID
                join roles in dbContext.Roles on userRoles.RoleID equals roles.ID
                select new UserRoleSummary
                {
                    ID = userRoles.ID,
                    UserID = userRoles.UserID,
                    RoleID = userRoles.RoleID,
                    UserName = users.Username,
                    RoleName = roles.Name
                }).ToList();

            return result;     
        }
    }
}