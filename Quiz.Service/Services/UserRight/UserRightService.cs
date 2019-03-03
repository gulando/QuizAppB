using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class UserRightService : IUserRightService
    {
        #region properties
        
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Right> _rightRepository;
        private readonly IRepository<UserRight> _userRightRepository;
        
        private readonly IRepositoryAsync<User> _userRepositoryAsync;
        private readonly IRepositoryAsync<Right> _rightRepositoryAsync;
        private readonly IRepositoryAsync<UserRight> _userRightRepositoryAsync;
        
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public UserRightService(IRepository<User> userRepository, IRepository<Right> rightRepository,
            IRepository<UserRight> userRightRepository, IRepositoryAsync<User> userRepositoryAsync,
            IRepositoryAsync<Right> rightRepositoryAsync, IRepositoryAsync<UserRight> userRightRepositoryAsync, 
            IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _rightRepository = rightRepository;
            _userRightRepository = userRightRepository;

            _userRepositoryAsync = userRepositoryAsync;
            _rightRepositoryAsync = rightRepositoryAsync;
            _userRightRepositoryAsync = userRightRepositoryAsync;
            
            _memoryCache = memoryCache;
        }

        #endregion

        #region methods
        
        public List<UserRight> GetAllUserRights()
        {
            if (_memoryCache.TryGetValue(UserRightDefaults.UserRightAllCacheKey, out List<UserRight> userRights)) 
                return userRights.ToList();
                
            userRights = _userRightRepository.Table.ToList();
            _memoryCache.Set(UserRightDefaults.UserRightAllCacheKey, userRights);

            return userRights;
        }

        public List<UserRightSummary> GetUserRightSummary(int roleRightID = 0)
        {
            var result = (from userRights in _userRightRepository.Table
                join users in _userRepository.Table on userRights.UserID equals users.ID
                join rights in _rightRepository.Table on userRights.RightID equals rights.ID
                select new UserRightSummary
                {
                    ID = userRights.ID,
                    UserID = userRights.UserID,
                    RightID = userRights.RightID,
                    UserName = users.Username,
                    RightName = rights.Name
                }).ToList();

            return result;  
        }
        
        public UserRight GetUserRightByID(int userRightID)
        {
            if (_memoryCache.TryGetValue(UserRightDefaults.UserRightByIdCacheKey, out UserRight userRight)) 
                return userRight;
            
            userRight = _userRightRepository.GetById(userRightID);
            _memoryCache.Set(UserRightDefaults.UserRightByIdCacheKey, userRight);

            return userRight;
        }

        public void UpdateUserRight(UserRight userRight)
        {
            _memoryCache.Remove(UserRightDefaults.UserRightAllCacheKey);
            _memoryCache.Remove(UserRightDefaults.UserRightByIdCacheKey);
            
            _userRightRepository.Update(userRight);
        }

        public void AddUserRight(UserRight userRight)
        {
            _memoryCache.Remove(UserRightDefaults.UserRightAllCacheKey);
            _memoryCache.Remove(UserRightDefaults.UserRightByIdCacheKey);
            
            _userRightRepository.Insert(userRight);
        }

        public void DeleteUserRight(int userRightID)
        {
            _memoryCache.Remove(UserRightDefaults.UserRightAllCacheKey);
            _memoryCache.Remove(UserRightDefaults.UserRightByIdCacheKey);
            
            _userRightRepository.Delete(userRightID);
        }
        
        #endregion
        
        #region async methods
        
        public async Task<List<UserRight>> GetAllUserRightsAsync()
        {
            if (_memoryCache.TryGetValue(UserRightDefaults.UserRightAllCacheKey, out List<UserRight> userRights)) 
                return userRights.ToList();
                
            userRights = await _userRightRepositoryAsync.Table.ToListAsync();
            _memoryCache.Set(UserRightDefaults.UserRightAllCacheKey, userRights);

            return userRights;
        }

        public async Task<List<UserRightSummary>> GetUserRightSummaryAsync(int userRightID = 0)
        {
            var result = (from userRights in _userRightRepositoryAsync.Table
                join users in _userRepositoryAsync.Table on userRights.UserID equals users.ID
                join rights in _rightRepositoryAsync.Table on userRights.RightID equals rights.ID
                select new UserRightSummary
                {
                    ID = userRights.ID,
                    UserID = userRights.UserID,
                    RightID = userRights.RightID,
                    UserName = users.Username,
                    RightName = rights.Name
                }).ToListAsync();

            return await result; 
        }

        public async Task<UserRight> GetUserRightByIDAsync(int userRightID)
        {
            if (_memoryCache.TryGetValue(UserRightDefaults.UserRightByIdCacheKey, out UserRight userRight)) 
                return userRight;
            
            userRight = await _userRightRepositoryAsync.GetByIdAsync(userRightID);
            _memoryCache.Set(UserRightDefaults.UserRightByIdCacheKey, userRight);

            return userRight;
        }

        public async Task AddUserRightAsync(UserRight userRight)
        {
            _memoryCache.Remove(UserRightDefaults.UserRightAllCacheKey);
            _memoryCache.Remove(UserRightDefaults.UserRightByIdCacheKey);
            
            await _userRightRepositoryAsync.InsertAsync(userRight);
        }

        public async Task UpdateUserRightAsync(UserRight userRight)
        {
            _memoryCache.Remove(UserRightDefaults.UserRightAllCacheKey);
            _memoryCache.Remove(UserRightDefaults.UserRightByIdCacheKey);
            
            await _userRightRepositoryAsync.UpdateAsync(userRight);
        }

        public async Task DeleteUserRightAsync(int userRightID)
        {
            _memoryCache.Remove(UserRightDefaults.UserRightAllCacheKey);
            _memoryCache.Remove(UserRightDefaults.UserRightByIdCacheKey);
            
            await _userRightRepositoryAsync.DeleteAsync(userRightID);
        }

        #endregion
    }
}