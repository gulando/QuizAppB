using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class UserRightService : IUserRightService
    {
        private readonly IUserRightRepository _userRightRepository; 

        public UserRightService(IUserRightRepository userRightRepository)
        {
            _userRightRepository = userRightRepository;
        }

        public IEnumerable<UserRight> UserRights => _userRightRepository.UserRights;
       
        public UserRight GetUserRightByID(int id)
        {
            return _userRightRepository.GetUserRightByID(id);
        }

        public UserRight Create(UserRight userRight)
        {
            return _userRightRepository.Create(userRight);
        }

        public void Update(UserRight userRight)
        {
            _userRightRepository.Update(userRight);
        }

        public void DeleteUserRight(int id)
        {
            _userRightRepository.DeleteUserRight(id);
        }
    }
}