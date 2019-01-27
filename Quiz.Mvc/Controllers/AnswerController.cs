using Microsoft.AspNetCore.Mvc;
using QuizData;
using QuizService;


namespace QuizMvc.Controllers
{
    public class AnswerController : Controller
    {
        #region properties
        
        private readonly IAnswerService _answerService;
        
        #endregion

        #region ctor
        
        public AnswerController(IAnswerService service)
        {
            _answerService = service;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var answers = _answerService.GetAnswerSummary();
            return View(answers); 
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _answerService.DeleteAnswer(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditAnswer", _answerService.GetAnswerByID(id));
        }

        [HttpPost]
        public IActionResult Edit(Answer answer)
        {
            _answerService.UpdateAnswer(answer);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditAnswer", new Answer());
        }
        
        [HttpPost]
        public IActionResult Create(Answer answer)
        {
            _answerService.AddAnswer(answer);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}