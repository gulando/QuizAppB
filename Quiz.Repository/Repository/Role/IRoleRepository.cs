using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public interface IRoleRepository
    {
        IEnumerable<Role> Roles { get;} 
        
        Role GetRoleByID(int id);
        
        Role Create(Role role);

        void Update(Role role);
        
        void DeleteRole(int id);
    }
}