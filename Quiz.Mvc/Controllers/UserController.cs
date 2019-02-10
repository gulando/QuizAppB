using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizData;
using QuizMvc.Helpers;
using QuizMvc.Models;
using QuizService;


namespace QuizMvc.Controllers
{
    public class UserController : Controller
    {
        #region properties
        
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;


        
        #endregion

        #region ctor
        
        public UserController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        
        #endregion
        
        #region actions

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public JsonResult Authenticate([FromBody]UserData userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return new JsonResult(new {message = "Username or password is incorrect"});

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new JsonResult(new {
                Id = user.ID,
                user.Username,
                user.FirstName,
                user.LastName,
                Token = tokenString
            });
        }
        
        public IActionResult Index()
        {
            var users = _userService.Users;
            var userDataList = _mapper.Map<IEnumerable<UserData>>(users);
            return View(userDataList);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            var user = _userService.GetUserByID(id);
            var userData = _mapper.Map<UserData>(user);
            return View("EditUser", userData);
        }

        [HttpPost]
        public IActionResult Edit(UserData userData)
        {
            var user = _mapper.Map<User>(userData);
            
            _userService.Update(user, userData.Password);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("EditUser", new UserData());
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create(UserData userData)
        {
            var user = _mapper.Map<User>(userData);
            
            _userService.Create(user,userData.Password);
            
            return RedirectToAction(nameof(Index));
        }
        
        #endregion
    }
}