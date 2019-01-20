using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;

namespace QuizMvc.Controllers
{
    public class AnswerController : Controller
    {
        #region properties
        
        private readonly IAnswerRepository _answerRepository;
        
        #endregion

        #region ctor
        
        public AnswerController(IAnswerRepository repo)
        {
            _answerRepository = repo;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var answers = _answerRepository.Answers;
            return View(answers); 
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _answerRepository.DeleteAnswer(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditAnswer", _answerRepository.GetAnswerByID(id));
        }

        [HttpPost]
        public IActionResult Edit(Answer answer)
        {
            _answerRepository.UpdateAnswer(answer);
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
            _answerRepository.AddAnswer(answer);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}