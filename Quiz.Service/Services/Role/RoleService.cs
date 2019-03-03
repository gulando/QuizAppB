using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class RoleService : IRoleService
    {
        #region properties

        private readonly IRepository<Role> _roleRepository;
        private readonly IRepositoryAsync<Role> _roleRepositoryAsync;
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor
        
        public RoleService(IRepository<Role> roleRepository,IRepositoryAsync<Role> roleRepositoryAsync, IMemoryCache memoryCache)
        {
            _roleRepository = roleRepository;
            _roleRepositoryAsync = roleRepositoryAsync;
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
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            _roleRepository.Update(role);
        }

        public void AddRole(Role role)
        {
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            _roleRepository.Insert(role);
        }

        public void DeleteRole(int roleID)
        {
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            _roleRepository.Delete(roleID);
        }

        #endregion
        
        #region async methods
        
        public async Task<List<Role>> GetAllRolesAsync()
        {
            if (_memoryCache.TryGetValue(RoleDefaults.RoleAllCacheKey, out List<Role> roles)) 
                return roles.ToList();
                
            roles = await _roleRepositoryAsync.Table.ToListAsync();
            _memoryCache.Set(RoleDefaults.RoleAllCacheKey, roles);

            return roles.ToList();
        }

        public async Task<Role> GetRoleByIDAsync(int roleID)
        {
            if (_memoryCache.TryGetValue(RoleDefaults.RoleByIdCacheKey, out Role role)) 
                return role;
            
            role = await _roleRepositoryAsync.GetByIdAsync(roleID);
            _memoryCache.Set(RoleDefaults.RoleByIdCacheKey, role);

            return role;
        }

        public async Task AddRoleAsync(Role role)
        {
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            await _roleRepositoryAsync.InsertAsync(role);
        }

        public async Task UpdateRoleAsync(Role role)
        {
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            await _roleRepositoryAsync.UpdateAsync(role);
        }

        public async Task DeleteRoleAsync(int roleID)
        {
            _memoryCache.Remove(RoleDefaults.RoleAllCacheKey);
            _memoryCache.Remove(RoleDefaults.RoleByIdCacheKey);
            
            await _roleRepositoryAsync.DeleteAsync(roleID);
        }

        #endregion
    }
}