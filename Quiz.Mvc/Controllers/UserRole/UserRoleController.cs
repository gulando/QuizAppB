using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizData;
using QuizMvc.Models;
using QuizService;


namespace QuizMvc.Controllers
{
    [Authorize]
    public class UserRoleController : Controller
    {
        #region properties
        
        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        #endregion

        #region ctor
        
        public UserRoleController(IUserRoleService userRoleService,IUserService userService,IRoleService roleService, IMapper mapper)
        {
            _userRoleService = userRoleService;
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region  basic actions
        
        public IActionResult Index()
        {
            var userRoles = _userRoleService.GetUserRoleDataList();
            var userRoleDataList = _mapper.Map<IEnumerable<UserRoleData>>(userRoles);
            
            return View(userRoleDataList);
        }
        
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;

            var userRoleSummaryList = _userRoleService.GetUserRoleDataList(); 
            var userRoleSummary = userRoleSummaryList.First(userRole => userRole.ID == id);
            
            var userData = _mapper.Map<UserRoleData>(userRoleSummary);

            ViewData["Roles"] = _roleService.Roles.ToList();
            ViewData["Users"] = _userService.Users.ToList();
            
            return View("EditUserRole", userData);
        }
        
        [HttpPost]
        public IActionResult Edit(UserRoleData userRoleData)
        {
            var userRole = _mapper.Map<UserRole>(userRoleData);
            
            _userRoleService.Update(userRole);
            
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            var userRoleData = new UserRoleData();
            
            ViewData["Roles"] = _roleService.Roles.ToList();
            ViewData["Users"] = _userService.Users.ToList();
            
            return View("EditUserRole", userRoleData );
        }
        
        [HttpPost]
        public IActionResult Create(UserRoleData userRoleData)
        {
            var userRole = _mapper.Map<UserRole>(userRoleData);
            _userRoleService.Create(userRole);
            
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userRoleService.DeleteUserRole(id);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}