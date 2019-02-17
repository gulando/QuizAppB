using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;
using QuizMvc.Models;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class QuestionTypeController : Controller
    {
        #region properties
        
        private readonly IQuestionTypeService _questionTypeService;
        private readonly IQuizService _quizService;
        private readonly IMapper _mapper;
        
        
        #endregion

        #region ctor
        
        public QuestionTypeController(IQuestionTypeService service, IQuizService quizService, IMapper mapper)
        {
            _questionTypeService = service;
            _quizService = quizService;
            _mapper = mapper;
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
            ViewData["Quizes"] = _quizService.Quizes.ToList();

            var questionType = _questionTypeService.GetQuestionTypeSummary(id).First();
            var questionTypeData = _mapper.Map<QuestionTypeData>(questionType);
            
            return View("EditQuestionType", questionTypeData);
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
            ViewData["Quizes"] = _quizService.Quizes.ToList();

            return View("EditQuestionType", new QuestionTypeData());
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