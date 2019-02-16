using System.Collections.Generic;
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
    }
}