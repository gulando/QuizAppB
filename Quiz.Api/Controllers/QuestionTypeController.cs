using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;


namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class QuestionTypeController : Controller
    {
        
        #region properties
        
        private readonly IQuestionTypeService _questionTypeService;
        
        #endregion

        #region ctor
        
        public QuestionTypeController(IQuestionTypeService service)
        {
            _questionTypeService = service;
        }
        
        #endregion
        
        #region api methods
        
        [HttpGet("{questionTypeID}")]
        [Produces("application/json")]
        [ActionName("GetQuestionTypeByID")]
        public JsonResult GetQuestionTypeByID(int questionTypeID) => Json(_questionTypeService.GetQuestionTypeByID(questionTypeID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuestionTypes")]
        public JsonResult GetAllQuestionTypes() => Json(_questionTypeService.QuestionTypes.ToList());

        [HttpPost]
        [ActionName("AddQuestionType")]
        public IActionResult AddQuestionType([FromBody] QuestionType res)
        {
            try
            {
                _questionTypeService.AddQuestionType(new QuestionType
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
                _questionTypeService.UpdateQuestionType(new QuestionType
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
                _questionTypeService.DeleteQuestionType(questionTypeID);
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