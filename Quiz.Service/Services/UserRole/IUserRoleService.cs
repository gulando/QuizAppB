using System.Collections.Generic;
using QuizData;


namespace QuizService
{
    public interface IUserRoleService
    {
        List<UserRole> GetAllUserRoles();

        UserRole GetUserRoleByID(int userRoleID);

        void UpdateUserRole(UserRole userRole);

        void AddUserRole(UserRole userRole);

        void DeleteUserRole(int userRoleID);
        
        List<UserRoleSummary> GetUserRoleSummary(int userRoleID = 0);
    }
}