using Microsoft.AspNetCore.Mvc;
using QuizData;
using QuizMvc.Helpers;
using QuizService;

namespace QuizMvc.ViewComponents
{
    public class CheckBoxComponent : ViewComponent
    {
        private readonly IAnswerTypeService _answerTypeService;
        private AnswerTypeConfiguration _answerTypeConfiguration { get; set; }

        public CheckBoxComponent(IAnswerTypeService answerTypeService)
        {
            _answerTypeService = answerTypeService;
        }

        public IViewComponentResult Invoke(int answerTypeID)
        {
            if (answerTypeID > 0)
            {
                var answerType = _answerTypeService.GetAnswerTypeByID(answerTypeID);
                var answerTypeDescriptionElement = answerType.AnswerTypeDescription;
                _answerTypeConfiguration = Util.Deserialize<AnswerTypeConfiguration>(answerTypeDescriptionElement);
            }

            return View("CheckBoxComponent", _answerTypeConfiguration);
        }
    }
}