using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizMvc.Controllers
{
    public class QuestionController : Controller
    {
        #region properties
        
        private readonly IQuestionRepository _questionRepository;
        
        #endregion

        #region ctor
        
        public QuestionController(IQuestionRepository repo)
        {
            _questionRepository = repo;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var questions = _questionRepository.Questions;
            return View(questions);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _questionRepository.DeleteQuestion(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditQuestion", _questionRepository.GetQuestionByID(id));
        }

        [HttpPost]
        public IActionResult Edit(Question question)
        {
            _questionRepository.UpdateQuestion(question);
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
            _questionRepository.AddQuestion(question);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
} 