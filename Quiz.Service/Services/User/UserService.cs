using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using QuizData;
using QuizRepository;
using QuizUtils;


namespace QuizService
{
    public class UserService : IUserService
    {
        #region properties

        private readonly IRepository<User> _userRepository;
        private readonly IMemoryCache _memoryCache;

        #endregion
        
        #region ctor
        
        public UserService(IRepository<User> userRepository, IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        #endregion
        
        #region methods

        public List<User> GetAllUsers()
        {
            if (_memoryCache.TryGetValue(UserDefaults.UserAllCacheKey, out List<User> users)) 
                return users.ToList();
                
            users = _userRepository.Table.ToList();
            _memoryCache.Set(UserDefaults.UserAllCacheKey, users);

            return users.ToList();
        }

        public User GetUserByID(int userID)
        {
            if (_memoryCache.TryGetValue(UserDefaults.UserByIdCacheKey, out User user)) 
                return user;
            
            user = _userRepository.GetById(userID);
            _memoryCache.Set(UserDefaults.UserByIdCacheKey, user);

            return user;
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void AddUser(User user)
        {
            _userRepository.Insert(user);
        }

        public void DeleteUser(int userID)
        {
            _userRepository.Delete(userID);
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepository.Table.First(name => name.Username == username);
            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!PasswordUtil.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public void Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("Password is required");

            if (_userRepository.Table.Any(x => x.Username == user.Username))
                throw new ApplicationException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            PasswordUtil.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            AddUser(user);
        }

        public void Update(User userParam, string password = null)
        {
            var user = _userRepository.GetById(userParam.ID);

            if (user == null)
                throw new ApplicationException("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_userRepository.Table.Any(x => x.Username == userParam.Username))
                    throw new ApplicationException("Username " + userParam.Username + " is already taken");
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;
            
            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                user.Password = password;
                
                byte[] passwordHash, passwordSalt;
                PasswordUtil.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            UpdateUser(user);
        }
             
        #endregion
        
    }
}