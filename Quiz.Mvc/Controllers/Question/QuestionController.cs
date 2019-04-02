using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using QuizData;
using QuizMvc.Handlers.ImageHandler;
using QuizMvc.Models;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        #region properties
        
        private readonly IQuestionService _questionService;
        private readonly IQuizService _quizService;
        private readonly IQuizThemeService _quizThemeService;
        private readonly IAnswerTypeService _answerTypeService;
        private readonly IQuestionTypeService _questionTypeService;
        private readonly IMapper _mapper;
        private readonly IImageService _imageHandler;
        
        private List<Quiz> Quizzes => _quizService.GetAllQuizes().ToList(); 
        private List<QuestionType> QuestionTypes => _questionTypeService.GetAllQuestionTypes().ToList();
        private List<QuizTheme> QuizThemes => _quizThemeService.GetAllQuizThemes().ToList();
        private List<AnswerType> AnswerTypes => _answerTypeService.GetAllAnswerTypes().ToList();
        
        #endregion

        #region ctor

        public QuestionController(IQuestionService service, IQuizService quizService,
            IQuizThemeService quizThemeService, IAnswerTypeService answerTypeService,
            IQuestionTypeService questionTypeService, IMapper mapper, IImageService imageHandler)
        {
            _questionService = service;
            _quizService = quizService;
            _quizThemeService = quizThemeService;
            _answerTypeService = answerTypeService;
            _questionTypeService = questionTypeService;
            _mapper = mapper;
            _imageHandler = imageHandler;
        }
        
        #endregion

        #region actions
        
        public IActionResult Index()
        {
            var questions = _questionService.GetQuestionSummary();
            return View(questions);
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            
            var quizList = Quizzes;
            quizList.Insert(0, new Quiz());
            ViewData["Quizes"] = quizList;

            var questionTypes = QuestionTypes;
            questionTypes.Insert(0, new QuestionType());
            ViewData["QuestionTypes"] = questionTypes;
            
            var quizThemesList = QuizThemes;
            quizThemesList.Insert(0, new QuizTheme());
            ViewData["QuizThemes"] = quizThemesList;

            var answerTypes = AnswerTypes;
            answerTypes.Insert(0, new AnswerType());
            ViewData["AnswerTypes"] = answerTypes;
            
            return View("EditQuestion", new QuestionData());
        }
        
        [HttpPost]
        public IActionResult Create(Question question, IFormFile file)
        {
            // _imageHandler.UploadImage(file, question.ImageID);
            
            if (file != null)
            {
                var image = CreateImage(file);
                var imageID = _imageHandler.InsertImageGetID(image);
                question.ImageID = imageID;
            }
                
            _questionService.AddQuestion(question);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            
            var question = _questionService.GetQuestionSummary(id).First();
            var questionData = _mapper.Map<QuestionData>(question);

            ViewData["Quizes"] = Quizzes;
            ViewData["QuizThemes"] = QuizThemes.Where(quizTheme => quizTheme.QuizID == questionData.QuizID);
            ViewData["QuestionTypes"] = QuestionTypes.Where(questionType => questionType.QuizID == questionData.QuizID);
            ViewData["AnswerTypes"] = AnswerTypes.Where(answerType => answerType.QuestionTypeID == questionData.QuestionTypeID);

            return View("EditQuestion", questionData);
        }

        [HttpPost]
        public IActionResult Edit(Question question, IFormFile file)
        {
            // _imageHandler.UploadImage(file, question.ImageID);
            
            if (file != null)
            {
                var image = CreateImage(file);
                var imageID = _imageHandler.InsertImageGetID(image);
                question.ImageID = imageID;
            }

            _questionService.UpdateQuestion(question);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _questionService.DeleteQuestion(id);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
        
        #region methods
        
        private Image CreateImage(IFormFile file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.OpenReadStream().CopyTo(ms);

                Image imageEntity = new Image()
                {
                    Name = file.Name,
                    Data = ms.ToArray(),
                    Length = file.Length,
                    ContentType = file.ContentType
                };

                return imageEntity;
            }
        }
        
        [HttpGet]
        public FileContentResult ViewImage(int imageID)
        {
            var image = _imageHandler.GetImageByID(imageID);
            
            using (MemoryStream ms = new MemoryStream(image.Data))
            {
                return new FileContentResult(ms.ToArray(), image.ContentType);
            }
            
        }
        
        #endregion
    }
}