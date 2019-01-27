using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;


namespace QuizMvc.Controllers
{
    public class QuestionController : Controller
    {
        #region properties
        
        private readonly IQuestionService _questionService;
        
        #endregion

        #region ctor
        
        public QuestionController(IQuestionService service)
        {
            _questionService = service;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var questions = _questionService.GetQuestionSummary();
            return View(questions);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _questionService.DeleteQuestion(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditQuestion", _questionService.GetQuestionByID(id));
        }

        [HttpPost]
        public IActionResult Edit(Question question)
        {
            _questionService.UpdateQuestion(question);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditQuestion", new Question());
        }
        
        [HttpPost]
        public IActionResult Create(Question question)
        {
            _questionService.AddQuestion(question);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
} 