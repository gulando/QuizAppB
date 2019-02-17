using System.Collections.Generic;
using QuizData;


namespace QuizRepository
{
    public interface IRightRepository
    {
        IEnumerable<Right> Rights { get;} 
        
        Right GetRightByID(int id);
        
        Right Create(Right right);

        void Update(Right right);
        
        void DeleteRight(int id);
    }
}