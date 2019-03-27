using System;
using System.Collections.Generic;
using AutoMapper;
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
        private readonly IMapper _mapper;
        
        #endregion

        #region ctor
        
        public QuizController(IQuizService service, IMapper mapper)
        {
            _quizService = service;
            _mapper = mapper;
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
        
        [HttpGet]
        public ActionResult GetQuizSummary(int quizID)
        {
            var quizSummary = _quizService.GetQuizSummary(quizID);
            var quizData = _mapper.Map<List<QuizSummary>, List<Models.QuizData>>(quizSummary);
            
            return Json(quizData);
        }
        
        #endregion
    }
}