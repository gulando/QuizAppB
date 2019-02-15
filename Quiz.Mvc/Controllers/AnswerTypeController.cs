using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizData;
using QuizService;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class AnswerTypeController : Controller
    {
        #region properties
        
        private readonly IAnswerTypeService _answerTypeService;
        
        #endregion

        #region ctor
        
        public AnswerTypeController(IAnswerTypeService service)
        {
            _answerTypeService = service;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var answerTypes = _answerTypeService.GetAnswerTypeSummary();
            return View(answerTypes);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _answerTypeService.DeleteAnswerType(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditAnswerType", _answerTypeService.GetAnswerTypeSummary(id).First());
        }

        [HttpPost]
        public IActionResult Edit(AnswerType answerType)
        {
            _answerTypeService.UpdateAnswerType(answerType);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditAnswerType", new AnswerTypeSummary());
        }
        
        [HttpPost]
        public IActionResult Create(AnswerType answerType)
        {
            _answerTypeService.AddAnswerType(answerType);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}