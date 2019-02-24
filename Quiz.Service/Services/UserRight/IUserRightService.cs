using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IUserRightService
    {
        List<UserRight> GetAllUserRights();

        UserRight GetUserRightByID(int userRightID);

        void UpdateUserRight(UserRight userRight);

        void AddUserRight(UserRight userRight);

        void DeleteUserRight(int userRightID);
        
        List<UserRightSummary> GetUserRightSummary(int roleRightID = 0);
    }
}