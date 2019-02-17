using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository; 

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<Role> Roles => _roleRepository.Roles;
       
        public Role GetRoleByID(int id)
        {
            return _roleRepository.GetRoleByID(id);
        }

        public Role Create(Role role)
        {
            return _roleRepository.Create(role);
        }

        public void Update(Role role)
        {
            _roleRepository.Update(role);
        }

        public void DeleteRole(int id)
        {
            _roleRepository.DeleteRole(id);
        }
    }
}