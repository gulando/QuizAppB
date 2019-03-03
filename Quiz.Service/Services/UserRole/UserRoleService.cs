using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class UserRoleService : IUserRoleService
    {
        #region properties
        
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        
        private readonly IRepositoryAsync<User> _userRepositoryAsync;
        private readonly IRepositoryAsync<Role> _roleRepositoryAsync;
        private readonly IRepositoryAsync<UserRole> _userRoleRepositoryAsync;
        
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public UserRoleService(IRepository<User> userRepository, IRepository<Role> roleRepository,
            IRepository<UserRole> userRoleRepository, IRepositoryAsync<User> userRepositoryAsync,
            IRepositoryAsync<Role> roleRepositoryAsync,IRepositoryAsync<UserRole> userRoleRepositoryAsync, 
            IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;

            _userRepositoryAsync = userRepositoryAsync;
            _roleRepositoryAsync = roleRepositoryAsync;
            _userRoleRepositoryAsync = userRoleRepositoryAsync;
            
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods
        
        public List<UserRole> GetAllUserRoles()
        {
            if (_memoryCache.TryGetValue(UserRoleDefaults.UserRoleAllCacheKey, out List<UserRole> userRoles)) 
                return userRoles.ToList();
                
            userRoles = _userRoleRepository.Table.ToList();
            _memoryCache.Set(UserRoleDefaults.UserRoleAllCacheKey, userRoles);

            return userRoles;
        }

        public UserRole GetUserRoleByID(int userRoleID)
        {
            if (_memoryCache.TryGetValue(UserRoleDefaults.UserRoleByIdCacheKey, out UserRole userRole)) 
                return userRole;
            
            userRole = _userRoleRepository.GetById(userRoleID);
            _memoryCache.Set(UserRoleDefaults.UserRoleByIdCacheKey, userRole);

            return userRole;
        }

        public void UpdateUserRole(UserRole userRole)
        {
            _memoryCache.Remove(UserRoleDefaults.UserRoleAllCacheKey);
            _memoryCache.Remove(UserRoleDefaults.UserRoleByIdCacheKey);
            
            _userRoleRepository.Update(userRole);
        }

        public void AddUserRole(UserRole userRole)
        {
            _memoryCache.Remove(UserRoleDefaults.UserRoleAllCacheKey);
            _memoryCache.Remove(UserRoleDefaults.UserRoleByIdCacheKey);
            
            _userRoleRepository.Insert(userRole);
        }

        public void DeleteUserRole(int userRoleID)
        {
            _memoryCache.Remove(UserRoleDefaults.UserRoleAllCacheKey);
            _memoryCache.Remove(UserRoleDefaults.UserRoleByIdCacheKey);
            
            _userRoleRepository.Delete(userRoleID);
        }

        public List<UserRoleSummary> GetUserRoleSummary(int userRoleID = 0)
        {
            var result = (from userRoles in _userRoleRepository.Table
                join users in _userRepository.Table on userRoles.UserID equals users.ID
                join roles in _roleRepository.Table on userRoles.RoleID equals roles.ID
                select new UserRoleSummary
                {
                    ID = userRoles.ID,
                    UserID = userRoles.UserID,
                    RoleID = userRoles.RoleID,
                    UserName = users.Username,
                    RoleName = roles.Name
                }).ToList();

            return result;  
        }
        
        #endregion
        
        #region async methods
        
        public async Task<List<UserRole>> GetAllUserRolesAsync()
        {
            if (_memoryCache.TryGetValue(UserRoleDefaults.UserRoleAllCacheKey, out List<UserRole> userRoles)) 
                return userRoles.ToList();
                
            userRoles = await _userRoleRepositoryAsync.Table.ToListAsync();
            _memoryCache.Set(UserRoleDefaults.UserRoleAllCacheKey, userRoles);

            return userRoles;
        }

        public async Task<List<UserRoleSummary>> GetUserRoleSummaryAsync(int userRoleID = 0)
        {
            var result = (from userRoles in _userRoleRepositoryAsync.Table
                join users in _userRepositoryAsync.Table on userRoles.UserID equals users.ID
                join roles in _roleRepositoryAsync.Table on userRoles.RoleID equals roles.ID
                select new UserRoleSummary
                {
                    ID = userRoles.ID,
                    UserID = userRoles.UserID,
                    RoleID = userRoles.RoleID,
                    UserName = users.Username,
                    RoleName = roles.Name
                }).ToListAsync();

            return await result;  
        }

        public async Task<UserRole> GetUserRoleByIDAsync(int userRoleID)
        {
            if (_memoryCache.TryGetValue(UserRoleDefaults.UserRoleByIdCacheKey, out UserRole userRole)) 
                return userRole;
            
            userRole = await _userRoleRepositoryAsync.GetByIdAsync(userRoleID);
            _memoryCache.Set(UserRoleDefaults.UserRoleByIdCacheKey, userRole);

            return userRole;
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            _memoryCache.Remove(UserRoleDefaults.UserRoleAllCacheKey);
            _memoryCache.Remove(UserRoleDefaults.UserRoleByIdCacheKey);
            
            await _userRoleRepositoryAsync.InsertAsync(userRole);
        }

        public async Task UpdateUserRoleAsync(UserRole userRole)
        {
            _memoryCache.Remove(UserRoleDefaults.UserRoleAllCacheKey);
            _memoryCache.Remove(UserRoleDefaults.UserRoleByIdCacheKey);
            
            await _userRoleRepositoryAsync.UpdateAsync(userRole);
        }

        public async Task DeleteUserRoleAsync(int userRoleID)
        {
            _memoryCache.Remove(UserRoleDefaults.UserRoleAllCacheKey);
            _memoryCache.Remove(UserRoleDefaults.UserRoleByIdCacheKey);
            
            await _userRoleRepositoryAsync.DeleteAsync(userRoleID);
        }
        
        #endregion
    }
}