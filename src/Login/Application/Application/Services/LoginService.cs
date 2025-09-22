using AutoMapper.Configuration;
using LoginApplication.Services.Base;
using McsUserLogs.Services.Base;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Services;
using UserApplication.Services.Base;
using Microsoft.Extensions.Configuration;
using TokenInformation.Base;

namespace LoginApplication.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserService _userService;
        private readonly string _jwtKey;

        public LoginService(IUserService userService, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _userService = userService;
            _jwtKey = configuration["jwt:Key"];
        }

        public async Task<bool> ValidateUser(string userName, string password)
        {
            var user = await _userService.GetUserByName(userName);
            if (user == null)
            {
                return false;
            }

            if (user.UserName == userName && _userService.Decrypt(user.Password)== password)
            {
                return true;
            }
            return false;
        }

        public string GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Task<bool> LogOutAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
