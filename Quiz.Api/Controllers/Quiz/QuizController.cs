using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        
        #endregion

        #region ctor
        
        public QuizController(IQuizService service, IMapper mapper)
        {
            _quizService = service;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{quizID}")]
        [Produces("application/json")]
        [ActionName("GetQuizByID")]
        public async Task<JsonResult> GetQuizByID(int quizID)
        {
            var quiz = await _quizService.GetQuizByIDAsync(quizID);
            if (quiz != null)
            {
                var quizData = _mapper.Map<Models.Quiz.QuizData>(quiz);
                return Json(quizData);
            }
                
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuizes")]
        public async Task<JsonResult> GetAllQuizes()
        {
            var quizList = await _quizService.GetAllQuizesAsync();
            if (quizList != null && quizList.Count > 0)
            {
                var quizListData = _mapper.Map<List<Quiz>, List<Models.Quiz.QuizData>>(quizList);
                return Json(quizListData);
            }
                
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddQuiz")]
        public async Task<JsonResult> AddQuiz([FromBody] Models.Quiz.QuizData quizData)
        {
            var quiz = _mapper.Map<Quiz>(quizData);
            await _quizService.AddQuizAsync(quiz);

            return new JsonResult("Quiz Added successfully");
        }

        [HttpPut("{quizID}")]
        [ActionName("UpdateQuiz")]
        public async Task<JsonResult> UpdateQuiz([FromBody] Models.Quiz.QuizData quizData)
        {
            var quiz = _mapper.Map<Quiz>(quizData);
            await _quizService.UpdateQuizAsync(quiz);

            return new JsonResult("Quiz Updated successfully");
        }
        
        [HttpDelete("{quizID}")]
        [ActionName("DeleteQuiz")]
        public async Task<JsonResult> DeleteQuiz(int quizID)
        {
            await _quizService.DeleteQuizAsync(quizID);

            return new JsonResult("Quiz Deleted successfully");
        }

        #endregion

        #region other

        [Produces("application/json")]
        [ActionName("GetAllQuizesWithChild")]
        public JsonResult GetAllQuizesWithChild()
        {
            var quizList = _quizService.GetAllQuizesEF();
            if (quizList != null)
            {
                var quizListData = _mapper.Map<List<Quiz>, List<Models.Quiz.QuizData>>(quizList);
                return Json(quizListData);
            }
                
            return new JsonResult(null);
        }

        #endregion
    }
}