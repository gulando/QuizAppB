using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IRoleRightService
    {
        List<RoleRight> GetAllRoleRights();

        RoleRight GetRoleRightByID(int roleRightID);

        void UpdateRoleRight(RoleRight roleRight);

        void AddRoleRight(RoleRight roleRight);

        void DeleteRoleRight(int roleRightID);
        
        List<RoleRightSummary> GetRoleRightSummary(int roleRightID = 0);
    }
}