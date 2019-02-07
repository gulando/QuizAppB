using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get;}
        
        User GetUserByID(int id);
        
        User Create(User user);

        void Update(User user);
        
        void DeleteUser(int id);
    }
}