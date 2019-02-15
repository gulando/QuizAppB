using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizData;
using QuizService;


namespace QuizApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class RightController : Controller
    {
        
        #region properties
        
        private readonly IRightService _rightService;
        private readonly IMapper _mapper;
                
        #endregion

        #region ctor
        
        public RightController(IRightService rightService, IMapper mapper)
        {
            _rightService = rightService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods
        
        [HttpGet("{rightID}")]
        [Produces("application/json")]
        [ActionName("GetRightByID")]
        public JsonResult GetRightByID(int rightID) => Json(_rightService.GetRightByID(rightID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllRights")] 
        public JsonResult GetAllRights() => Json(_rightService.Rights.ToList());

        [HttpPost]
        [ActionName("AddRight")]
        public IActionResult AddRight([FromBody] Right right)
        {
            try
            {
                return new OkObjectResult(_rightService.Create(right));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{rightID}")]
        [ActionName("UpdateRight")]
        public IActionResult UpdateRight(int rightID, [FromBody] Right right)
        {
            try
            {
                _rightService.Update(new Right
                {
                    ID =  rightID,
                    Name = right.Name,
                    Description =  right.Description
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete("{rightID}")]
        [ActionName("DeleteRight")]
        public IActionResult DeleteRight(int rightID)
        {
            try
            {
                _rightService.DeleteRight(rightID);
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