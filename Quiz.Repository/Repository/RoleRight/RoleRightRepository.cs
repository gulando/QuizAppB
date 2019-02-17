using System.Collections.Generic;
using System.Linq;
using QuizData;


namespace QuizRepository
{
    public class RoleRightRepository : Repository<RoleRight>, IRoleRightRepository
    {
        public RoleRightRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<RoleRight> RoleRights => GetObjList();
        
        public RoleRight GetRoleRightByID(int id)
        {
            return GetObjByID(id);
        }

        public RoleRight Create(RoleRight roleRight)
        {
            return AddObj(roleRight);
        }

        public void Update(RoleRight roleRight)
        {
            UpdateObj(roleRight);
        }

        public void DeleteRoleRight(int id)
        {
            DeleteObj(id);
        }

        public List<RoleRightSummary> GetRoleRightSummary()
        {
            var result = (from roleRights in dbContext.RoleRights
                join roles in dbContext.Roles on roleRights.RoleID equals roles.ID
                join rights in dbContext.Rights on roleRights.RightID equals rights.ID
                select new RoleRightSummary
                {
                    ID = roleRights.ID,
                    RoleID = roles.ID,
                    RightID = rights.ID,
                    RoleName = roles.Name,
                    RightName = rights.Name
                }).ToList();

            return result;     
        }
    }
}