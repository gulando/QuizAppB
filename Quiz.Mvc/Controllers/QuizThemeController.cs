using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;


namespace QuizApi.Controllers
{
    [Authorize]
    public class QuizThemeController : Controller
    {
        #region properties
        
        private readonly IQuizThemeService _quizThemeService;
        
        #endregion

        #region ctor
        
        public QuizThemeController(IQuizThemeService service)
        {
            _quizThemeService = service;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var quizThemes = _quizThemeService.GetQuizThemeSummary();
            return View(quizThemes);
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _quizThemeService.DeleteQuizTheme(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(QuizTheme quizTheme)
        {
            _quizThemeService.UpdateQuizTheme(quizTheme);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditQuizTheme", _quizThemeService.GetQuizThemeByID(id));
        }

        [HttpPost]
        public IActionResult Create(QuizTheme quizTheme)
        {
            _quizThemeService.AddQuizTheme(quizTheme);
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