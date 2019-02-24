using System.Collections.Generic;
using System.Linq;
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
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor

        public UserRightService(IRepository<User> userRepository, IRepository<Right> rightRepository,
            IRepository<UserRight> userRightRepository, IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _rightRepository = rightRepository;
            _userRightRepository = userRightRepository;
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
            _userRightRepository.Update(userRight);
        }

        public void AddUserRight(UserRight userRight)
        {
            _userRightRepository.Insert(userRight);
        }

        public void DeleteUserRight(int userRightID)
        {
            _userRightRepository.Delete(userRightID);
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
        
        #endregion
    }
}