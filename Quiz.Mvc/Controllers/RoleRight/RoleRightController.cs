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
    public class RoleRightController : Controller
    {
        #region properties
        
        private readonly IRoleRightService _roleRightService;
        private readonly IRoleService _roleService;
        private readonly IRightService _rightService;
        private readonly IMapper _mapper;

        #endregion

        #region ctor
        
        public RoleRightController(IRoleRightService roleRightService,IRoleService roleService,IRightService rightService, IMapper mapper)
        {
            _roleRightService = roleRightService;
            _roleService = roleService;
            _rightService = rightService;
            _mapper = mapper;
        }
        
        #endregion
        
        #region  basic actions
        
        public IActionResult Index()
        {
            var roleRightSummary = _roleRightService.GetRoleRightSummary();
            var roleRightDataList = _mapper.Map<IEnumerable<RoleRightData>>(roleRightSummary);
            
            return View(roleRightDataList);
        }
        
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;

            var roleRightSummary = _roleRightService.GetRoleRightSummary().First(roleRight => roleRight.ID == id);
            var roleRightData = _mapper.Map<RoleRightData>(roleRightSummary);
            
            ViewData["Roles"] = _roleService.Roles.ToList();
            ViewData["Rights"] = _rightService.Rights.ToList();
            
            return View("EditRoleRight", roleRightData);
        }
        
        [HttpPost]
        public IActionResult Edit(RoleRightData roleRightData)
        {
            var roleRight = _mapper.Map<RoleRight>(roleRightData);
            _roleRightService.Update(roleRight);
            
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            ViewData["Roles"] = _roleService.Roles.ToList();
            ViewData["Rights"] = _rightService.Rights.ToList();

            var roleRightData = new RoleRightData();
            
            return View("EditRoleRight", roleRightData );
        }
        
        [HttpPost]
        public IActionResult Create(RoleRightData roleRightData)
        {
            var roleRight = _mapper.Map<RoleRight>(roleRightData);
            _roleRightService.Create(roleRight);
            
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _roleRightService.DeleteRoleRight(id);
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}