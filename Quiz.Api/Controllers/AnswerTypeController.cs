using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
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
        
        #region api methods
        
        [HttpGet("{answerTypeID}")]
        [Produces("application/json")]
        public JsonResult GetAnswerType(int answerTypeID) => Json(_answerTypeRepository.GetAnswerTypeByID(answerTypeID));

        [HttpGet]
        [Produces("application/json")]
        public JsonResult GetAll() => Json(_answerTypeRepository.AnswerTypes.ToList());

        [HttpPost]
        public IActionResult AddAnswerType([FromBody] AnswerType res)
        {
            try
            {
                _answerTypeRepository.SaveAnswerType(new AnswerType
                {
                    QuizTD = res.QuizTD,
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
        public IActionResult UpdateAnswerType(int answerTypeID, [FromBody] AnswerType res)
        {
            try
            {
                _answerTypeRepository.SaveAnswerType(new AnswerType
                {
                    AnswerTypeID = answerTypeID,
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
        public IActionResult DeleteAnswerType(int answerTypeID)
        {
            try
            {
                _answerTypeRepository.DeleteAnswerType(answerTypeID);
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