using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizMvc.Controllers
{
    public class QuizController : Controller
    {
        
        #region properties
        
        private readonly IQuizRepository _quizRepository;
        
        #endregion

        #region ctor
        
        public QuizController(IQuizRepository repo)
        {
            _quizRepository = repo;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var quizes = _quizRepository.Quizes;
            return View(quizes);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _quizRepository.DeleteQuiz(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditQuiz", _quizRepository.GetQuizByID(id));
        }

        [HttpPost]
        public IActionResult Edit(Quiz quiz)
        {
            _quizRepository.UpdateQuiz(quiz);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditQuiz", new Quiz());
        }
        
        [HttpPost]
        public IActionResult Create(Quiz quiz)
        {
            _quizRepository.AddQuiz(quiz);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}