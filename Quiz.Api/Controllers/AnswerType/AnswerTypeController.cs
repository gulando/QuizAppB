using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Models;
using QuizService;
using QuizData;


namespace QuizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AnswerTypeController : Controller
    {
        
        #region properties
        
        private readonly IAnswerTypeService _answerTypeService;
        private readonly IMapper _mapper;
        
        #endregion

        #region ctor
        
        public AnswerTypeController(IAnswerTypeService service,  IMapper mapper)
        {
            _answerTypeService = service;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods

        [HttpGet("{answerTypeID}")]
        [Produces("application/json")]
        [ActionName("GetAnswerTypeByID")]
        public async Task<JsonResult> GetAnswerType(int answerTypeID)
        {
            var answer = await _answerTypeService.GetAnswerTypeSummaryAsync(answerTypeID);
            if (answer != null && answer.Count > 0)
            {
                var answerTypeData = _mapper.Map<AnswerTypeData>(answer.First());
                return Json(answerTypeData);                
            }
            return new JsonResult(null);
        }

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllAnswerTypes")]
        public async Task<JsonResult> GetAllAnswerTypes()
        {
            var answerTypes = await _answerTypeService.GetAnswerTypeSummaryAsync();
            if (answerTypes != null && answerTypes.Count > 0)
            {
                var answerDataList = _mapper.Map<List<AnswerTypeSummary>, List<AnswerTypeData>>(answerTypes);
                return Json(answerDataList);
            }
            return new JsonResult(null);
        }

        [HttpPost]
        [ActionName("AddAnswerType")]
        public async Task<JsonResult> AddAnswerType([FromBody] AnswerType answerTypeData)
        {
            var answerType = _mapper.Map<AnswerType>(answerTypeData);
            await _answerTypeService.AddAnswerTypeAsync(answerType);
            return new JsonResult(null);
        }

        [HttpPut("{answerTypeID}")]
        [ActionName("UpdateAnswerType")]
        public async Task<JsonResult> UpdateAnswerType(int answerTypeID, [FromBody] AnswerType answerTypeData)
        {
            var answer = _mapper.Map<AnswerType>(answerTypeData);
            await _answerTypeService.UpdateAnswerTypeAsync(answer);
            return new JsonResult(null);
        }
        
        [HttpDelete("{answerTypeID}")]
        [ActionName("DeleteAnswerType")]
        public async Task<JsonResult> DeleteAnswerType(int answerTypeID)
        {
            await _answerTypeService.DeleteAnswerTypeAsync(answerTypeID);
            return new JsonResult(null);
        }
        
        #endregion
        
    }
}