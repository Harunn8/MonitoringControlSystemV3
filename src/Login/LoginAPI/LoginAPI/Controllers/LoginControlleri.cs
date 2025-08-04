using LoginApplication.Models;
using LoginApplication.Services;
using LoginApplication.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LoginAPI.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _login;

        public LoginController(ILoginService login)
        {
            _login = login;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var isValidUser = await _login.ValidateUser(request.Username, request.Password);

            if (isValidUser)
            {
                var token = _login.GenerateJwtToken(request.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password");
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
