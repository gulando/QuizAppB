using System;
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
                return Json(quiz);
            
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuizes")]
        public async Task<JsonResult> GetAllQuizes()
        {
            var quizList = await _quizService.GetAllQuizesAsync();
            if (quizList != null && quizList.Count > 0)
                return Json(quizList);
            
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddQuiz")]
        public async Task<JsonResult> AddQuiz([FromBody] Quiz quiz)
        {
            await _quizService.AddQuizAsync(quiz);
            return new JsonResult(null);
        }

        [HttpPut("{quizID}")]
        [ActionName("UpdateQuiz")]
        public async Task<JsonResult> UpdateQuiz(int quizID, [FromBody] Quiz quiz)
        {
            await _quizService.UpdateQuizAsync(quiz);
            return new JsonResult(null);
        }
        
        [HttpDelete("{quizID}")]
        [ActionName("DeleteQuiz")]
        public async Task<JsonResult> DeleteQuiz(int quizID)
        {
            await _quizService.DeleteQuizAsync(quizID);
            return new JsonResult(null);
        }

        #endregion

        #region other

        [Produces("application/json")]
        [ActionName("GetAllQuizesEF")]
        public JsonResult GetQuizzes()
        {
            var quiz = _quizService.GetAllQuizesEF();
            if (quiz != null)
                return Json(quiz);

            return new JsonResult(null);
        }

        #endregion
    }
}