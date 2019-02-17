using System.Collections.Generic;
using System.Linq;
using QuizRepository;
using QuizData;


namespace QuizService
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository; 

        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }
        
        public IEnumerable<Answer> Answers => _answerRepository.Answers;
        
        public void UpdateAnswer(Answer answer)
        {
            _answerRepository.UpdateAnswer(answer);
        }

        public void AddAnswer(Answer answer)
        {
            _answerRepository.AddAnswer(answer);
        }

        public Answer DeleteAnswer(int answerID)
        {
            return _answerRepository.DeleteAnswer(answerID);
        }

        public Answer GetAnswerByID(int answerID)
        {
            return _answerRepository.GetAnswerByID(answerID);
        }

        public List<AnswerSummary> GetAnswerSummary(int answerID = 0)
        {
            return _answerRepository.GetAnswerSummary(answerID);
        }
    }
}