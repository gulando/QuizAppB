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
    public class RoleController : Controller
    {
        
        #region properties
        
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
                
        #endregion

        #region ctor
        
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region api methods
        
        [HttpGet("{roleID}")]
        [Produces("application/json")]
        [ActionName("GetRoleByID")]
        public JsonResult GetRoleByID(int roleID) => Json(_roleService.GetRoleByID(roleID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllRoles")] 
        public JsonResult GetAllRoles() => Json(_roleService.GetAllRoles().ToList());

        [HttpPost]
        [ActionName("AddRole")]
        public IActionResult AddRole([FromBody] Role role)
        {
            try
            {
                _roleService.AddRole(role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

        [HttpPut("{roleID}")]
        [ActionName("UpdateRole")]
        public IActionResult UpdateRole(int roleID, [FromBody] Role role)
        {
            try
            {
                _roleService.UpdateRole(new Role
                {
                    ID =  roleID,
                    Name = role.Name,
                    Description =  role.Description
                });
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete("{roleID}")]
        [ActionName("DeleteRole")]
        public IActionResult DeleteRole(int roleID)
        {
            try
            {
                _roleService.DeleteRole(roleID);
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