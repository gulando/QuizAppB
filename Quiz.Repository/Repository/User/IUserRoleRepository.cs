using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public interface IUserRoleRepository
    {
        IEnumerable<UserRole> UserRoles { get;} 
        
        UserRole GetUserRoleByID(int id);
        
        UserRole Create(UserRole userRole);

        void Update(UserRole userRole);
        
        void DeleteUserRole(int id);
        
        List<UserRoleSummary> GetUserRoleDataList();

    }
}