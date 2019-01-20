using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
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
        [ActionName("GetQuestionTypeByID")]
        public JsonResult GetQuestionTypeByID(int questionTypeID) => Json(_questionTypeRepository.GetQuestionByID(questionTypeID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuestionTypes")]
        public JsonResult GetAllQuestionTypes() => Json(_questionTypeRepository.QuestionTypes.ToList());

        [HttpPost]
        [ActionName("AddQuestionType")]
        public IActionResult AddQuestionType([FromBody] QuestionType res)
        {
            try
            {
                _questionTypeRepository.AddQuestionType(new QuestionType
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
        [ActionName("UpdateQuestionType")]
        public IActionResult UpdateQuestionType(int questionTypeID, [FromBody] QuestionType res)
        {
            try
            {
                _questionTypeRepository.UpdateQuestionType(new QuestionType
                {
                    QuizID = res.QuizID,
                    ID = questionTypeID,
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
        [ActionName("DeleteQuestionType")]
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