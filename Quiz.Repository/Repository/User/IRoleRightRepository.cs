using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public interface IRoleRightRepository
    {
        IEnumerable<RoleRight> RoleRights { get;} 
        
        RoleRight GetRoleRightByID(int id);
        
        RoleRight Create(RoleRight roleRight);

        void Update(RoleRight roleRight);
        
        void DeleteRoleRight(int id);
    }
}