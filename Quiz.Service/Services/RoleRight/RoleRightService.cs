using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class RoleRightService : IRoleRightService
    {
        #region properties
        
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Right> _rightRepository;
        private readonly IRepository<RoleRight> _roleRightRepository;
        
        private readonly IRepositoryAsync<Role> _roleRepositoryAsync;
        private readonly IRepositoryAsync<Right> _rightRepositoryAsync;
        private readonly IRepositoryAsync<RoleRight> _roleRightRepositoryAsync;
        
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public RoleRightService(IRepository<Role> roleRepository, IRepository<Right> rightRepository,
            IRepository<RoleRight> roleRightRepository, IRepositoryAsync<Role> roleRepositoryAsync,
            IRepositoryAsync<Right> rightRepositoryAsync,IRepositoryAsync<RoleRight> roleRightRepositoryAsync, 
            IMemoryCache memoryCache)
        {
            _roleRepository = roleRepository;
            _rightRepository = rightRepository;
            _roleRightRepository = roleRightRepository;

            _roleRepositoryAsync = roleRepositoryAsync;
            _rightRepositoryAsync = rightRepositoryAsync;
            _roleRightRepositoryAsync = roleRightRepositoryAsync;
            
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods

        public List<RoleRight> GetAllRoleRights()
        {
            if (_memoryCache.TryGetValue(RoleRightDefaults.RoleRightAllCacheKey, out List<RoleRight> roleRights)) 
                return roleRights.ToList();
                
            roleRights = _roleRightRepository.Table.ToList();
            _memoryCache.Set(RoleRightDefaults.RoleRightAllCacheKey, roleRights);

            return roleRights;
        }

        public RoleRight GetRoleRightByID(int roleRightID)
        {
            if (_memoryCache.TryGetValue(RoleRightDefaults.RoleRightByIdCacheKey, out RoleRight roleRight)) 
                return roleRight;
            
            roleRight = _roleRightRepository.GetById(roleRightID);
            _memoryCache.Set(RoleRightDefaults.RoleRightByIdCacheKey, roleRight);

            return roleRight;
        }

        public void UpdateRoleRight(RoleRight roleRight)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            _roleRightRepository.Update(roleRight);
        }

        public void AddRoleRight(RoleRight roleRight)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            _roleRightRepository.Insert(roleRight);
        }

        public void DeleteRoleRight(int roleRightID)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            _roleRightRepository.Delete(roleRightID);
        }

        public List<RoleRightSummary> GetRoleRightSummary(int roleRightID = 0)
        {
            var result = (from roleRights in _roleRightRepository.Table
                join roles in _roleRepository.Table on roleRights.RoleID equals roles.ID
                join rights in _rightRepository.Table on roleRights.RightID equals rights.ID
                select new RoleRightSummary
                {
                    ID = roleRights.ID,
                    RoleID = roles.ID,
                    RightID = rights.ID,
                    RoleName = roles.Name,
                    RightName = rights.Name
                }).ToList();

            return result;     
        }
        
        #endregion

        #region async methods
        
        public async Task<List<RoleRight>> GetAllRoleRightsAsync()
        {
            if (_memoryCache.TryGetValue(RoleRightDefaults.RoleRightAllCacheKey, out List<RoleRight> roleRights)) 
                return roleRights.ToList();
                
            roleRights = await _roleRightRepositoryAsync.Table.ToListAsync();
            _memoryCache.Set(RoleRightDefaults.RoleRightAllCacheKey, roleRights);

            return roleRights;
        }

        public async Task<List<RoleRightSummary>> GetRoleRightSummaryAsync(int roleRightID = 0)
        {
            var result = (from roleRights in _roleRightRepositoryAsync.Table
                join roles in _roleRepositoryAsync.Table on roleRights.RoleID equals roles.ID
                join rights in _rightRepositoryAsync.Table on roleRights.RightID equals rights.ID
                select new RoleRightSummary
                {
                    ID = roleRights.ID,
                    RoleID = roles.ID,
                    RightID = rights.ID,
                    RoleName = roles.Name,
                    RightName = rights.Name
                }).ToListAsync();

            return await result;  
        }

        public async Task<RoleRight> GetRoleRightByIDAsync(int roleRightID)
        {
            if (_memoryCache.TryGetValue(RoleRightDefaults.RoleRightByIdCacheKey, out RoleRight roleRight)) 
                return roleRight;
            
            roleRight = await _roleRightRepositoryAsync.GetByIdAsync(roleRightID);
            _memoryCache.Set(RoleRightDefaults.RoleRightByIdCacheKey, roleRight);

            return roleRight;
        }

        public async Task AddRoleRightAsync(RoleRight roleRight)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            await _roleRightRepositoryAsync.InsertAsync(roleRight);
        }

        public async Task UpdateRoleRightAsync(RoleRight roleRight)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            await _roleRightRepositoryAsync.UpdateAsync(roleRight);
        }

        public async Task DeleteRoleRightAsync(int roleRightID)
        {
            _memoryCache.Remove(RoleRightDefaults.RoleRightAllCacheKey);
            _memoryCache.Remove(RoleRightDefaults.RoleRightByIdCacheKey);
            
            await _roleRightRepositoryAsync.DeleteAsync(roleRightID);
        }

        #endregion
    }
}