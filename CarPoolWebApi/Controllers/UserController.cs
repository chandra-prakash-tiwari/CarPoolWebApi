using CarPoolingWebApi.Models.Client;
using CarPoolingWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarPoolWebApi.Controllers
{
    [Route("api/user/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService;
        }

        [Authorize(Roles ="Admin,User")]
        [HttpGet]
        [ActionName("getuser")]
        public IActionResult GetUser(string id)
        {
            User user = _UserService.GetUser(id);

            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            return Ok(user);
        }

        [HttpPost]
        [ActionName("newuser")]
        public IActionResult AddNewUser([FromBody] User user)
        {
            if (user == null)
            {
                return NoContent();
            }

            _UserService.AddNewUser(user);
            return Ok(user);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete]
        [ActionName("delete")]
        public IActionResult DeleteUser(string id)
        {
            User user = _UserService.GetUser(id);
            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            _UserService.DeleteUser(id);
            return NoContent();
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        [ActionName("update")]
        public IActionResult UpdateUser(string id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Employee is null.");
            }

            User old = _UserService.GetUser(id);
            if (old == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            _UserService.UpdateUser(user, id);
            return NoContent();
        }

        
        //public IActionResult Authentication2([FromBody] Login login)
        //{
        //    //CarPoolingWebApi.Models.Data.User user = _UserService.Authentication(login);
        //    if (user == null)
        //    {
        //        return Unauthorized("UserName Or Password is wrong");
        //    }

        //    return Ok(user);
        //}

        [AllowAnonymous]
        [HttpPost]
        [ActionName("authenticate")]
        public IActionResult Authentication([FromBody] Login login)
        {
            var user = _UserService.Authentication(login);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var key = Convert.FromBase64String(Convert.ToString(Guid.NewGuid()));
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor securityToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims: new[] { new Claim(type: ClaimTypes.Name, value: login.UserName) }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(securityKey, algorithm: SecurityAlgorithms.HmacSha256)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenString = handler.CreateJwtSecurityToken(securityToken);

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes("a");
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.Id.ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                id = user.Id,
                Username = user.UserName,
                name = user.Name,
                address = user.Address,
                token = tokenString
            });
        }

        
        [AllowAnonymous]
        [HttpPost]
        [ActionName("usernameavailability")]
        public IActionResult UserNameAvailability([FromBody] string userName)
        {
            if (_UserService.CheckUserName(userName))
            {
                return BadRequest("This username taken by some use another one");
            }
            return Ok();
        }
    }
}