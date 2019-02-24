using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;


namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AnswerController : Controller
    {
        
        #region properties
        
        private readonly IAnswerService _answerService;
        
        #endregion

        #region ctor
        
        public AnswerController(IAnswerService service)
        {
            _answerService = service;
        }
        
        #endregion
        
        #region api methods
        
        [HttpGet("{answerID}")]
        [Produces("application/json")]
        [ActionName("GetAnswerByID")]
        public JsonResult GetAnswer(int answerID) => Json(_answerService.GetAnswerByID(answerID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllAnswers")]
        public JsonResult GetAllAnswers() => Json(_answerService.GetAllAnswers().ToList());

        [HttpPost]
        [ActionName("AddAnswer")]
        public IActionResult AddAnswer([FromBody] Answer res)
        {
            try
            {
                _answerService.AddAnswer(new Answer
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
        [ActionName("UpdateAnswer")]
        public IActionResult UpdateAnswer(int answerID, [FromBody] Answer res)
        {
            try
            {
                _answerService.UpdateAnswer(new Answer
                {
                    ID = answerID,
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
        [ActionName("DeleteAnswer")]
        public IActionResult DeleteAnswer(int answerID)
        {
            try
            {
                _answerService.DeleteAnswer(answerID);
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