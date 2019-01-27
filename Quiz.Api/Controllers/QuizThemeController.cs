using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;


namespace QuizApi.Controllers
{
    
    [Route("api/[controller]/[action]")]
    public class QuizThemeController : Controller
    {
        
        #region properties
        
        private readonly IQuizThemeService _quizThemeService;
        
        #endregion

        #region ctor
        
        public QuizThemeController(IQuizThemeService service)
        {
            _quizThemeService = service;
        }
        
        #endregion
        
        #region api methods
        
        [HttpGet("{quizThemeID}")]
        [Produces("application/json")]
        [ActionName("GetQuizThemeByID")]
        public JsonResult GetQuizTheme(int quizThemeID) => Json(_quizThemeService.GetQuizThemeByID(quizThemeID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuizThemes")]
        public JsonResult GetAllQuizThemes() => Json(_quizThemeService.QuizeThemes.ToList());

        [HttpPost]
        [ActionName("AddQuizTheme")]
        public IActionResult AddQuizTheme([FromBody] QuizTheme res)
        {
            try
            {
                _quizThemeService.AddQuizTheme(new QuizTheme
                {
                    QuizID = res.QuizID,
                    QuizThemeName = res.QuizThemeName
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{quizThemeID}")]
        [ActionName("UpdateQuizTheme")]
        public IActionResult UpdateQuizTheme(int quizThemeID, [FromBody] QuizTheme res)
        {
            try
            {
                _quizThemeService.UpdateQuizTheme(new QuizTheme
                {
                    ID =  quizThemeID,
                    QuizThemeName = res.QuizThemeName
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete("{quizThemeID}")]
        [ActionName("DeleteQuizTheme")]
        public IActionResult DeleteQuizTheme(int quizThemeID)
        {
            try
            {
                _quizThemeService.DeleteQuizTheme(quizThemeID);
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