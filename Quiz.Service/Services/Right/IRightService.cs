using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public interface IRightService
    {
        List<Right> GetAllRights();

        Right GetRightByID(int rightID);

        void UpdateRight(Right right);

        void AddRight(Right right);

        void DeleteRight(int rightID);

    }
}