using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class RoleService : IRoleService
    {
        #region properties

        private readonly IRepository<Role> _roleRepository;
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor
        
        public RoleService(IRepository<Role> roleRepository, IMemoryCache memoryCache)
        {
            _roleRepository = roleRepository;
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods
        
        public List<Role> GetAllRoles()
        {
            if (_memoryCache.TryGetValue(RoleDefaults.RoleAllCacheKey, out List<Role> roles)) 
                return roles.ToList();
                
            roles = _roleRepository.Table.ToList();
            _memoryCache.Set(RoleDefaults.RoleAllCacheKey, roles);

            return roles.ToList();
        }

        public Role GetRoleByID(int roleID)
        {
            if (_memoryCache.TryGetValue(RoleDefaults.RoleByIdCacheKey, out Role role)) 
                return role;
            
            role = _roleRepository.GetById(roleID);
            _memoryCache.Set(RoleDefaults.RoleByIdCacheKey, role);

            return role;
        }

        public void UpdateRole(Role role)
        {
            _roleRepository.Update(role);
        }

        public void AddRole(Role role)
        {
            _roleRepository.Insert(role);
        }

        public void DeleteRole(int roleID)
        {
            _roleRepository.Delete(roleID);
        }
        
        #endregion
    }
}