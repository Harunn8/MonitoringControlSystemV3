using LoginApplication.Models;
using LoginApplication.Services;
using LoginApplication.Services.Base;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var result = await _login.Login(request);
            
            if(result == null) return Unauthorized("Invalid username or password");

            return Ok(result);
        }
    }
}
