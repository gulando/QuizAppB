using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class QuestionTypeController : Controller
    {
        #region properties
        
        private readonly IQuestionTypeService _questionTypeService;
        
        #endregion

        #region ctor
        
        public QuestionTypeController(IQuestionTypeService service)
        {
            _questionTypeService = service;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var questionTypes = _questionTypeService.GetQuestionTypeSummary();
            return View(questionTypes);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _questionTypeService.DeleteQuestionType(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditQuestionType", _questionTypeService.GetQuestionTypeByID(id));
        }

        [HttpPost]
        public IActionResult Edit(QuestionType questionType)
        {
            _questionTypeService.UpdateQuestionType(questionType);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditQuestionType", new QuestionType());
        }
        
        [HttpPost]
        public IActionResult Create(QuestionType questionType)
        {
            _questionTypeService.AddQuestionType(questionType);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}