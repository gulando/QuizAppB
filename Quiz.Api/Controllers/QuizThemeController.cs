using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizApi.Controllers
{
    
    [Route("api/[controller]/[action]")]
    public class QuizThemeController : Controller
    {
        
        #region properties
        
        private readonly IQuizThemeRepository _quizThemeRepository;
        
        #endregion

        #region ctor
        
        public QuizThemeController(IQuizThemeRepository repo)
        {
            _quizThemeRepository = repo;
        }
        
        #endregion
        
        #region api methods
        
        [HttpGet("{quizThemeID}")]
        [Produces("application/json")]
        [ActionName("GetQuizThemeByID")]
        public JsonResult GetQuizTheme(int quizThemeID) => Json(_quizThemeRepository.GetQuizThemeByID(quizThemeID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllQuizThemes")]
        public JsonResult GetAllQuizThemes() => Json(_quizThemeRepository.QuizeThemes.ToList());

        [HttpPost]
        [ActionName("AddQuizTheme")]
        public IActionResult AddQuizTheme([FromBody] QuizTheme res)
        {
            try
            {
                _quizThemeRepository.AddQuizTheme(new QuizTheme
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
                _quizThemeRepository.UpdateQuizTheme(new QuizTheme
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
                _quizThemeRepository.DeleteQuizTheme(quizThemeID);
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