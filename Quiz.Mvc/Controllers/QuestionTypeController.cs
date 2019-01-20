using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizMvc.Controllers
{
    public class QuestionTypeController : Controller
    {
        #region properties
        
        private readonly IQuestionTypeRepository _questionTypeRepository;
        
        #endregion

        #region ctor
        
        public QuestionTypeController(IQuestionTypeRepository repo)
        {
            _questionTypeRepository = repo;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var questionTypes = _questionTypeRepository.QuestionTypes;
            return View(questionTypes);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _questionTypeRepository.DeleteQuestionType(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditQuestionType", _questionTypeRepository.GetQuestionByID(id));
        }

        [HttpPost]
        public IActionResult Edit(QuestionType questionType)
        {
            _questionTypeRepository.UpdateQuestionType(questionType);
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
            _questionTypeRepository.AddQuestionType(questionType);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}