using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizData;
using QuizMvc.Models;
using QuizService;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class AnswerTypeController : Controller
    {
        #region properties
        
        private readonly IAnswerTypeService _answerTypeService;
        private readonly IQuizService _quizService;
        private readonly IQuestionTypeService _questionTypeService;
        private readonly IMapper _mapper;
        
        #endregion

        #region ctor
        
        public AnswerTypeController(IAnswerTypeService service, IQuizService quizService, IQuestionTypeService questionTypeService, IMapper mapper)
        {
            _answerTypeService = service;
            _quizService = quizService;
            _questionTypeService = questionTypeService;
            _mapper = mapper;
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
            
            ViewData["Quizes"] = _quizService.GetAllQuizes().ToList();
            ViewData["QuestionTypes"] = _questionTypeService.GetAllQuestionTypes().ToList();
            
            var answerTypeSummary = _answerTypeService.GetAnswerTypeSummary(id).First();
            var answerTypeData = _mapper.Map<AnswerTypeData>(answerTypeSummary);
            
            return View("EditAnswerType", answerTypeData);
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
            
            var quizList =  _quizService.GetAllQuizes().ToList();
            quizList.Insert(0, new Quiz());
            ViewData["Quizes"] = quizList;
            
            var questionTypes = _questionTypeService.GetAllQuestionTypes().ToList();
            questionTypes.Insert(0, new QuestionType());
            ViewData["QuestionTypes"] = questionTypes;
            
            return View("EditAnswerType", new AnswerTypeData());
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