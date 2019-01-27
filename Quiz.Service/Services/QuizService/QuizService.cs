using System.Collections.Generic;
using System.Linq;
using QuizRepository;


namespace QuizService
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository; 

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public IEnumerable<QuizData.Quiz> Quizes => _quizRepository.Quizes;
        
        public QuizData.Quiz GetQuizByID(int quizID)
        {
            return _quizRepository.GetQuizByID(quizID);
        }

        public void AddQuiz(QuizData.Quiz quiz)
        {
            _quizRepository.AddQuiz(quiz);
        }

        public void UpdateQuiz(QuizData.Quiz quiz)
        {
            _quizRepository.UpdateQuiz(quiz);
        }

        public QuizData.Quiz DeleteQuiz(int quizID)
        {
            return _quizRepository.DeleteQuiz(quizID);
        }
    }
}