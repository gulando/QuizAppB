using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
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
        
        #region api methods
        
        [HttpGet("{questionTypeID}")]
        [Produces("application/json")]
        public JsonResult GetQuestionType(int questionTypeID) => Json(_questionTypeRepository.GetQuestionByID(questionTypeID));

        [HttpGet]
        [Produces("application/json")]
        public JsonResult GetAll() => Json(_questionTypeRepository.QuestionTypes.ToList());

        [HttpPost]
        public IActionResult AddQuestionType([FromBody] QuestionType res)
        {
            try
            {
                _questionTypeRepository.SaveQuestionType(new QuestionType
                {
                    QuizID = res.QuizID,
                    QuestionTypeName = res.QuestionTypeName
                   
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{questionTypeID}")]
        public IActionResult UpdateQuestionType(int questionTypeID, [FromBody] QuestionType res)
        {
            try
            {
                _questionTypeRepository.SaveQuestionType(new QuestionType
                {
                    QuizID = res.QuizID,
                    QuestionTypeID = questionTypeID,
                    QuestionTypeName = res.QuestionTypeName
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete("{questionTypeID}")]
        public IActionResult DeleteQuestionType(int questionTypeID)
        {
            try
            {
                _questionTypeRepository.DeleteQuestionType(questionTypeID);
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