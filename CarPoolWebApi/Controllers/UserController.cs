using CarPoolingWebApi.Models.Client;
using CarPoolingWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarPoolWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService;

        }

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

        [HttpPut]
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

        [AllowAnonymous]
        [HttpPost]
        [ActionName("authenticate")]
        public IActionResult Authentication([FromBody] Login login)
        {
            var user = _UserService.Authentication(login);

            return Ok(new
            {
                id = user.Id,
                Username = user.UserName,
                name = user.Name,
                address = user.Address,
                userToken = user.Token
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