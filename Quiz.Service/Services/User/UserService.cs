using System;
using System.Collections.Generic;
using System.Linq;
using QuizData;
using QuizRepository;
using QuizUtils;


namespace QuizService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; 

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> Users => _userRepository.Users;

        public User GetUserByID(int id)
        {
            return _userRepository.GetUserByID(id);
        }

        public User Create(User user)
        {
            return _userRepository.Create(user);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepository.Users.First(name => name.Username == username);
            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!PasswordUtil.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ApplicationException("Password is required");

            if (_userRepository.Users.Any(x => x.Username == user.Username))
                throw new ApplicationException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            PasswordUtil.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Create(user);
        }

        public void Update(User userParam, string password = null)
        {
            var user = _userRepository.GetUserByID(userParam.ID);

            if (user == null)
                throw new ApplicationException("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_userRepository.Users.Any(x => x.Username == userParam.Username))
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

            Update(user);
        }
    }
}