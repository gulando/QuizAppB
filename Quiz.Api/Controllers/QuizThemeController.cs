using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizData.Models;


namespace QuizApi.Controllers
{
    
    [Route("api/[controller]")]
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
        public JsonResult GetQuizTheme(int quizThemeID) => Json(_quizThemeRepository.GetQuizThemeByID(quizThemeID));

        [HttpGet]
        [Produces("application/json")]
        public JsonResult GetAll() => Json(_quizThemeRepository.QuizeThemes.ToList());

        [HttpPost]
        public IActionResult AddQuizTheme([FromBody] QuizTheme res)
        {
            try
            {
                _quizThemeRepository.SaveQuizTheme(new QuizTheme
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
        public IActionResult UpdateQuizTheme(int quizThemeID, [FromBody] QuizTheme res)
        {
            try
            {
                _quizThemeRepository.SaveQuizTheme(new QuizTheme
                {
                    QuizThemeID =  quizThemeID,
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