using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IUserService : IUserRepository
    {
        User Authenticate(string username, string password);
        
        User Create(User user, string password);
        
        void Update(User user, string password = null);
        
    }
}