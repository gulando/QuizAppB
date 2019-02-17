using System;
using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<Role> Roles => GetObjList();
        
        public Role GetRoleByID(int id)
        {
            return GetObjByID(id);
        }

        public Role Create(Role role)
        {
            return AddObj(role);
        }

        public void Update(Role role)
        {
            UpdateObj(role);
        }

        public void DeleteRole(int id)
        {
            DeleteObj(id);
        }
    }
}