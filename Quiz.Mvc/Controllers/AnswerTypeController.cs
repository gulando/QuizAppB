using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizMvc.Controllers
{
    public class AnswerTypeController : Controller
    {
        #region properties
        
        private readonly IAnswerTypeRepository _answerTypeRepository;
        
        #endregion

        #region ctor
        
        public AnswerTypeController(IAnswerTypeRepository repo)
        {
            _answerTypeRepository = repo;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var answerTypes = _answerTypeRepository.AnswerTypes;
            return View(answerTypes);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _answerTypeRepository.DeleteAnswerType(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("EditAnswerType", _answerTypeRepository.GetAnswerTypeByID(id));
        }

        [HttpPost]
        public IActionResult Edit(AnswerType answerType)
        {
            _answerTypeRepository.UpdateAnswerType(answerType);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditAnswerType", new AnswerType());
        }
        
        [HttpPost]
        public IActionResult Create(AnswerType answerType)
        {
            _answerTypeRepository.AddAnswerType(answerType);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}