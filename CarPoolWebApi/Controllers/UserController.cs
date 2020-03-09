using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPoolingEf.Models;
using CarPoolingEf.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace CarPoolWebApi.Controllers
{
    [Route("api/User/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices<User> _UserServices;

        public UserController(IUserServices<User> userServices)
        {
            _UserServices = userServices;
        }

        [HttpGet]
        [ActionName("GetUser")]
        public IActionResult GetUser(string id)
        {
            User user = _UserServices.GetUser(id);

            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            return Ok(user);
        }

        [HttpPost]
        [ActionName("NewUser")]
        public IActionResult AddNewUser([FromBody] User user)
        {
            if (user == null)
            {
                return NoContent();
            }

            _UserServices.AddNewUser(user);
            return Ok(user);
        }

        [HttpDelete]
        [ActionName("Delete")]
        public IActionResult DeleteUser(string id)
        {
            User user = _UserServices.GetUser(id);
            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            _UserServices.DeleteUser(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ActionName("Update")]
        public IActionResult UpdateUser(string id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Employee is null.");
            }

            User old = _UserServices.GetUser(id);
            if (old == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            _UserServices.UpdateUser(user, id);
            return NoContent();
        }

        [HttpPost]
        [ActionName("Authenticate")]
        public IActionResult Authentication([FromBody] Login login)
        {
            User user = _UserServices.Authentication(login);
            if (user == null)
            {
                return Unauthorized("UserName Or Password is wrong");
            }

            return Ok(user);
        }
        
        [HttpPost]
        [ActionName("UserNameAvailability")]
        public IActionResult UserNameAvailability([FromBody] string userName)
        {
            if (_UserServices.CheckUserName(userName))
            {
                return BadRequest("This username taken by some use another one");
            }
            return Ok();
        }
    }
}