using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserApplication.Services.Base;
using System.Threading.Tasks;
using System;
using UserApplication.Models;
using McsCore.Entities;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUser")]
        public async Task<ActionResult> GetAllUser()
        {
            var users = await _userService.GetAllUserAsync();
            return Ok(users);
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("GetUserByName")]
        public async Task<ActionResult> GetUserByName(string userName)
        {
            var user = await _userService.GetUserByName(userName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] Users userModel)
        {
            if (userModel == null)
            {
                return BadRequest("User model cannot be null");
            }
            var createdUser = await _userService.CreateUser(userModel);
            return Ok(createdUser);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] Users userModel)
        {
            if (userModel == null)
            {
                return BadRequest("User model cannot be null");
            }
            await _userService.UpdateUser(id, userModel);
            return NoContent();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteUser(id);
            return NoContent();
        }

        [HttpGet("GetUserByNameAsync")]
        public async Task<ActionResult> GetUserByNameAsync(string userName)
        {
            var user = await _userService.GetUserByName(userName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}