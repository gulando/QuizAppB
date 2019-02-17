using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public interface IUserRightRepository
    {
        IEnumerable<UserRight> UserRights { get;} 
        
        UserRight GetUserRightByID(int id);
        
        UserRight Create(UserRight userRight);

        void Update(UserRight userRight);
        
        void DeleteUserRight(int id);
        
        List<UserRightSummary> GetUserRightSummary();
    }
}