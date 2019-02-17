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
    public class QuestionController : Controller
    {
        #region properties
        
        private readonly IQuestionService _questionService;
        private readonly IQuizService _quizService;
        private readonly IQuizThemeService _quizThemeService;
        private readonly IAnswerTypeService _answerTypeService;
        private readonly IQuestionTypeService _questionTypeService;
        private readonly IMapper _mapper;
        
        #endregion

        #region ctor
        
        public QuestionController(IQuestionService service, IQuizService quizService, IQuizThemeService quizThemeService, IAnswerTypeService answerTypeService, IQuestionTypeService questionTypeService, IMapper mapper)
        {
            _questionService = service;
            _quizService = quizService;
            _quizThemeService = quizThemeService;
            _answerTypeService = answerTypeService;
            _questionTypeService = questionTypeService;
            _mapper = mapper;
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
            
            ViewData["Quizes"] = _quizService.Quizes.ToList();
            ViewData["QuizThemes"] = _quizThemeService.QuizeThemes.ToList();
            ViewData["AnswerTypes"] = _answerTypeService.AnswerTypes.ToList();
            ViewData["QuestionTypes"] = _questionTypeService.QuestionTypes.ToList();
            ViewData["Questions"] = _questionService.Questions.ToList();

            var question = _questionService.GetQuestionSummary(id).First();
            var questionData = _mapper.Map<QuestionData>(question);
            
            
            return View("EditQuestion", questionData);
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
            
            ViewData["Quizes"] = _quizService.Quizes.ToList();
            ViewData["QuizThemes"] = _quizThemeService.QuizeThemes.ToList();
            ViewData["AnswerTypes"] = _answerTypeService.AnswerTypes.ToList();
            ViewData["QuestionTypes"] = _questionTypeService.QuestionTypes.ToList();
            ViewData["Questions"] = _questionService.Questions.ToList();
            
            return View("EditQuestion", new QuestionData());
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