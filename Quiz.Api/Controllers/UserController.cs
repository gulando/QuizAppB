using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Helpers;
using QuizApi.Models;
using QuizData;
using QuizService;


namespace QuizApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
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
        
        #region api methods
        
        [HttpGet("{userID}")]
        [Produces("application/json")]
        [ActionName("GetUserByID")]
        public JsonResult GetUserByID(int userID) => Json(_userService.GetUserByID(userID));

        [HttpGet]
        [Produces("application/json")]
        [ActionName("GetAllUsers")] 
        public JsonResult GetAllUsers() => Json(_userService.Users.ToList());

        [HttpPost]
        [ActionName("RegisterUser")]
        public IActionResult AddUser([FromBody] UserData userData)
        {
            try
            {
                var user = _mapper.Map<User>(userData);
                _userService.Create(user,userData.Password);
                return new OkObjectResult(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut]
        [ActionName("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserData userData)
        {
            try
            {
                var user = _mapper.Map<User>(userData);
                _userService.Update(user, userData.Password);
                
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete("{userID}")]
        [ActionName("DeleteUser")]
        public IActionResult DeleteUser(int userID)
        {
            try
            {
                _userService.DeleteUser(userID);
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
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
        
        #endregion
    }
}