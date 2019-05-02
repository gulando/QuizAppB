using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QuizApi.Controllers.ExamType
{
    [Route("api/[controller]/[action]")]
    public class ExamTypeController : Controller
    {
        #region properties

        private readonly IExamTypeService _examTypeService;
        private readonly IMapper _mapper;

        #endregion

        #region ctor

        public ExamTypeController(IExamTypeService service, IMapper mapper)
        {
            _examTypeService = service;
            _mapper = mapper;
        }

        #endregion

        #region actions

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllExamTypes")]
        public async Task<JsonResult> GetAllExamTypes()
        {
            var answerTypes = await _examTypeService.GetAllExamTypesAsync();

            if (answerTypes != null && answerTypes.Count > 0)
                return Json(answerTypes);
            
            return new JsonResult(null);
        }

        #endregion
    }
}
