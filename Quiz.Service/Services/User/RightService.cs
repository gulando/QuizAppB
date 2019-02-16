using System.Collections.Generic;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class RightService : IRightService
    {
        private readonly IRightRepository _rightRepository; 

        public RightService(IRightRepository rightRepository)
        {
            _rightRepository = rightRepository;
        }

        public IEnumerable<Right> Rights => _rightRepository.Rights;
       
        public Right GetRightByID(int id)
        {
            return _rightRepository.GetRightByID(id);
        }

        public Right Create(Right right)
        {
            return _rightRepository.Create(right);
        }

        public void Update(Right right)
        {
            _rightRepository.Update(right);
        }

        public void DeleteRight(int id)
        {
            _rightRepository.DeleteRight(id);
        }
    }
}