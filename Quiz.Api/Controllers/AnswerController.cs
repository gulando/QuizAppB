using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
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
        
        #region api methods
        
        [HttpGet("{answerID}")]
        [Produces("application/json")]
        public JsonResult GetAnswer(int answerID) => Json(_answerRepository.GetAnswerByID(answerID));

        [HttpGet]
        [Produces("application/json")]
        public JsonResult GetAll() => Json(_answerRepository.Answers.ToList());

        [HttpPost]
        public IActionResult AddAnswer([FromBody] Answer res)
        {
            try
            {
                _answerRepository.SaveAnswer(new Answer
                {
                    QuestionID = res.QuestionID,
                    AnswerTypeID = res.AnswerTypeID,
                    AnswerText = res.AnswerText
                });
                
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{answerID}")]
        public IActionResult UpdateAnswer(int answerID, [FromBody] Answer res)
        {
            try
            {
                _answerRepository.SaveAnswer(new Answer
                {
                    AnswerID = answerID,
                    QuestionID = res.QuestionID,
                    AnswerTypeID = res.AnswerTypeID,
                    AnswerText = res.AnswerText
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete("{answerID}")]
        public IActionResult DeleteAnswer(int answerID)
        {
            try
            {
                _answerRepository.DeleteAnswer(answerID);
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        #endregion
        
    }
}