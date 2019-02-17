using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository; 

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public IEnumerable<UserRole> UserRoles => _userRoleRepository.UserRoles;
       
        public UserRole GetUserRoleByID(int id)
        {
            return _userRoleRepository.GetUserRoleByID(id);
        }

        public UserRole Create(UserRole userRole)
        {
            return _userRoleRepository.Create(userRole);
        }

        public void Update(UserRole userRole)
        {
            _userRoleRepository.Update(userRole);
        }

        public void DeleteUserRole(int id)
        {
            _userRoleRepository.DeleteUserRole(id);
        }

        public List<UserRoleSummary> GetUserRoleDataList()
        {
            return _userRoleRepository.GetUserRoleDataList();
        }
    }
}