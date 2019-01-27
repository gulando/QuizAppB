using System.Collections.Generic;
using System.Linq;
using QuizData;
using QuizRepository;


namespace QuizService
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository; 

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public IEnumerable<Question> Questions => _questionRepository.Questions;
        
        public Question GetQuestionByID(int questionID)
        {
            return _questionRepository.GetQuestionByID(questionID);
        }

        public void AddQuestion(Question question)
        {
            _questionRepository.AddQuestion(question);
        }

        public void UpdateQuestion(Question question)
        {
            _questionRepository.UpdateQuestion(question);
        }

        public Question DeleteQuestion(int questionID)
        {
            return _questionRepository.DeleteQuestion(questionID);
        }

        public List<QuestionSummary> GetQuestionSummary()
        {
            return _questionRepository.GetQuestionSummary();
        }
    }
}