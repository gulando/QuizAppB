using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
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
        public JsonResult GetQuestion(int questionID) => Json(_questionRepository.GetQuestionByID(questionID));

        [HttpGet]
        [Produces("application/json")]
        public JsonResult GetAll() => Json(_questionRepository.Questions.ToList());

        [HttpPost]
        public IActionResult AddQuestion([FromBody] Question res)
        {
            try
            {
                _questionRepository.SaveQuestion(new Question
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
        public IActionResult UpdateQuestion(int questionID, [FromBody] Question res)
        {
            try
            {
                _questionRepository.SaveQuestion(new Question
                {
                    QuestionID =  questionID,
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