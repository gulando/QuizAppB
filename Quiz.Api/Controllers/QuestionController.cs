using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class QuestionController : Controller
    {
        
        #region properties
        
        private readonly IQuestionRepository _questionRepository;
        
        #endregion

        #region ctor
        
        public QuestionController(IQuestionRepository repo)
        {
            _questionRepository = repo;
        }
        
        #endregion
        
        #region api methods
        
        [HttpGet("{questionID}")]
        [Produces("application/json")]
        [ActionName("GetQuestionByID")]
        public JsonResult GetQuestionByID(int questionID) => Json(_questionRepository.GetQuestionByID(questionID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuestions")]
        public JsonResult GetAllQuestions() => Json(_questionRepository.Questions.ToList());

        [HttpPost]
        [ActionName("AddQuestion")]
        public IActionResult AddQuestion([FromBody] Question res)
        {
            try
            {
                _questionRepository.AddQuestion(new Question
                {
                    QuizID = res.QuizID,
                    QuizThemeID = res.QuizThemeID,
                    AnswerTypeID = res.AnswerTypeID,
                    QuestionTypeID = res.QuestionTypeID,
                    QuestionImage = res.QuestionImage,
                    CorrectAnswer = res.CorrectAnswer
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{questionID}")]
        [ActionName("UpdateQuestion")]
        public IActionResult UpdateQuestion(int questionID, [FromBody] Question res)
        {
            try
            {
                _questionRepository.UpdateQuestion(new Question
                {
                    ID =  questionID,
                    QuizID = res.QuizID,
                    QuizThemeID = res.QuizThemeID,
                    AnswerTypeID = res.AnswerTypeID,
                    QuestionTypeID = res.QuestionTypeID,
                    QuestionImage = res.QuestionImage,
                    CorrectAnswer = res.CorrectAnswer
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete("{questionID}")]
        [ActionName("DeleteQuestion")]
        public IActionResult DeleteQuestion(int questionID)
        {
            try
            {
                _questionRepository.DeleteQuestion(questionID);
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