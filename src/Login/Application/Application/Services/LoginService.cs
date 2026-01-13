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
using LoginApplication.Responses;
using McsCore.Entities;
using LoginApplication.Models;

namespace LoginApplication.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserService _userService;
        private readonly string _jwtKey;
        private readonly ITokenInformationService _tokenInformationService;

        public LoginService(IUserService userService, Microsoft.Extensions.Configuration.IConfiguration configuration,
            ITokenInformationService tokenInformationService)
        {
            _userService = userService;
            _jwtKey = configuration["jwt:Key"];
            _tokenInformationService = tokenInformationService;
        }

        public async Task<bool> ValidateUser(string userName, string password)
        {
            var user = await _userService.GetUserByName(userName);
            if (user == null) return false;

            if (user.UserName == userName && _userService.Decrypt(user.Password) == password) return true;
            return false;
        }

        public string GenerateJwtToken(string userName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(JwtRegisteredClaimNames.Email, $"{userName}@deneme.com")
            };
            
            var securityToken = new JwtSecurityToken
            (
                issuer: "McsGen3.com",
                audience: "McsGen3.com",
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
                    SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public Task<bool> LogOutAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponses> Login(LoginRequest request)
        {
            var user = await _userService.GetUserByName(request.UserName);

            if (user == null) return null;

            var validateUserResult = await ValidateUser(request.UserName, request.Password);

            if (!validateUserResult) return null;

            var token = GenerateJwtToken(request.UserName);

            var loginResponse = new LoginResponses(user, token);

            var userName = _tokenInformationService.GetUserName;

            return loginResponse;
        }
    }
}