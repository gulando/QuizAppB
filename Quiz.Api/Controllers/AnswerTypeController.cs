using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;


namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AnswerTypeController : Controller
    {
        
        #region properties
        
        private readonly IAnswerTypeService _answerTypeService;
        
        #endregion

        #region ctor
        
        public AnswerTypeController(IAnswerTypeService service)
        {
            _answerTypeService = service;
        }
        
        #endregion
        
        #region api methods
        
        [HttpGet("{answerTypeID}")]
        [Produces("application/json")]
        [ActionName("GetAnswerTypeByID")]
        public JsonResult GetAnswerType(int answerTypeID) => Json(_answerTypeService.GetAnswerTypeByID(answerTypeID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllAnswerTypes")]
        public JsonResult GetAllAnswerTypes() => Json(_answerTypeService.AnswerTypes.ToList());

        [HttpPost]
        [ActionName("AddAnswerType")]
        public IActionResult AddAnswerType([FromBody] AnswerType res)
        {
            try
            {
                _answerTypeService.AddAnswerType(new AnswerType
                {
                    QuizID = res.QuizID,
                    AnswerTypeName = res.AnswerTypeName
                });
                
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{answerTypeID}")]
        [ActionName("UpdateAnswerType")]
        public IActionResult UpdateAnswerType(int answerTypeID, [FromBody] AnswerType res)
        {
            try
            {
                _answerTypeService.UpdateAnswerType(new AnswerType
                {
                    ID = answerTypeID,
                    AnswerTypeName = res.AnswerTypeName
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete("{answerTypeID}")]
        [ActionName("DeleteAnswerType")]
        public IActionResult DeleteAnswerType(int answerTypeID)
        {
            try
            {
                _answerTypeService.DeleteAnswerType(answerTypeID);
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