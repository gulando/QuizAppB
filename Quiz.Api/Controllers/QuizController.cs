using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class QuizController : Controller
    {
        
        #region properties
        
        private readonly IQuizRepository _quizRepository;
        
        #endregion

        #region ctor
        
        public QuizController(IQuizRepository repo)
        {
            _quizRepository = repo;
        }
        
        #endregion
        
        #region api methods
        
        [HttpGet("{quizID}")]
        [Produces("application/json")]
        [ActionName("GetQuizByID")]
        public JsonResult GetQuizByID(int quizID) => Json(_quizRepository.GetQuizByID(quizID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuizes")] 
        public JsonResult GetAllQuizes() => Json(_quizRepository.Quizes.ToList());

        [HttpPost]
        [ActionName("AddQuiz")]
        public IActionResult AddQuiz([FromBody] Quiz res)
        {
            try
            {
                _quizRepository.AddQuiz(new Quiz
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
                _quizRepository.UpdateQuiz(new Quiz
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
                _quizRepository.DeleteQuiz(quizID);
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