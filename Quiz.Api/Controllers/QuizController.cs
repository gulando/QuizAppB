using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;


namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class QuizController : Controller
    {
        
        #region properties
        
        private readonly IQuizService _quizService;
        
        #endregion

        #region ctor
        
        public QuizController(IQuizService service)
        {
            _quizService = service;
        }
        
        #endregion
        
        #region api methods
        
        [HttpGet("{quizID}")]
        [Produces("application/json")]
        [ActionName("GetQuizByID")]
        public JsonResult GetQuizByID(int quizID) => Json(_quizService.GetQuizByID(quizID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuizes")] 
        public JsonResult GetAllQuizes() => Json(_quizService.GetAllQuizes().ToList());

        [HttpPost]
        [ActionName("AddQuiz")]
        public IActionResult AddQuiz([FromBody] Quiz res)
        {
            try
            {
                _quizService.AddQuiz(new Quiz
                {
                    QuizName = res.QuizName
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{quizID}")]
        [ActionName("UpdateQuiz")]
        public IActionResult UpdateQuiz(int quizID, [FromBody] Quiz res)
        {
            try
            {
                _quizService.UpdateQuiz(new Quiz
                {
                    ID =  quizID,
                    QuizName = res.QuizName
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete("{quizID}")]
        [ActionName("DeleteQuiz")]
        public IActionResult DeleteQuiz(int quizID)
        {
            try
            {
                _quizService.DeleteQuiz(quizID);
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