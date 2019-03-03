using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        
        #region properties
        
        private readonly IQuizService _quizService;
        
        #endregion

        #region ctor
        
        public QuizController(IQuizService service)
        {
            _quizService = service;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var quizes = _quizService.GetAllQuizes();
            return View(quizes);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _quizService.DeleteQuiz(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditQuiz", _quizService.GetQuizByID(id));
        }

        [HttpPost]
        public IActionResult Edit(Quiz quiz)
        {
            _quizService.UpdateQuiz(quiz);
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
            _quizService.AddQuiz(quiz);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}