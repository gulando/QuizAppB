using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IUserService
    {
        List<User> GetAllUsers();

        User GetUserByID(int userID);

        void UpdateUser(User user);

        void AddUser(User user);

        void DeleteUser(int userID);
        
        User Authenticate(string username, string password);
        
        void Create(User user, string password);
        
        void Update(User user, string password = null);
        
    }
}