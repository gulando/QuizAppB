using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class RoleRightService : IRoleRightService
    {
        private readonly IRoleRightRepository _roleRightRepository; 

        public RoleRightService(IRoleRightRepository roleRightRepository)
        {
            _roleRightRepository = roleRightRepository;
        }

        public IEnumerable<RoleRight> RoleRights => _roleRightRepository.RoleRights;
       
        public RoleRight GetRoleRightByID(int id)
        {
            return _roleRightRepository.GetRoleRightByID(id);
        }

        public RoleRight Create(RoleRight roleRight)
        {
            return _roleRightRepository.Create(roleRight);
        }

        public void Update(RoleRight roleRight)
        {
            _roleRightRepository.Update(roleRight);
        }

        public void DeleteRoleRight(int id)
        {
            _roleRightRepository.DeleteRoleRight(id);
        }

        public List<RoleRightSummary> GetRoleRightSummary()
        {
            return _roleRightRepository.GetRoleRightSummary();
        }
    }
}