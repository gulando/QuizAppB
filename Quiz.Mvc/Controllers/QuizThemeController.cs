using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizApi.Controllers
{
    public class QuizThemeController : Controller
    {
        #region properties
        
        private readonly IQuizThemeRepository _quizThemeRepository;
        
        #endregion

        #region ctor
        
        public QuizThemeController(IQuizThemeRepository repo)
        {
            _quizThemeRepository = repo;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var quizThemes = _quizThemeRepository.QuizeThemes;
            return View(quizThemes);
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _quizThemeRepository.DeleteQuizTheme(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(QuizTheme quizTheme)
        {
            _quizThemeRepository.UpdateQuizTheme(quizTheme);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditQuizTheme", _quizThemeRepository.GetQuizThemeByID(id));
        }

        [HttpPost]
        public IActionResult Create(QuizTheme quizTheme)
        {
            _quizThemeRepository.AddQuizTheme(quizTheme);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditQuizTheme", new QuizTheme());
        }
        
        #endregion

    }
}