using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IRoleService 
    {
        List<Role> GetAllRoles();

        Role GetRoleByID(int roleID);

        void UpdateRole(Role role);

        void AddRole(Role role);

        void DeleteRole(int roleID);
    }
}