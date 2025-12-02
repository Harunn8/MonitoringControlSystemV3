using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TokenInformation.Base
{
    public interface ITokenInformationService
    {
        HttpContext Current { get; }
        string GetToken();
        string GetUserNameFromToken(string token);
        Guid GetUserId { get; }
        string GetUserName { get; }
        string GetName { get; }
    }

    public class TokenInformationService : ITokenInformationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenInformationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext Current => _httpContextAccessor.HttpContext;

        public string GetToken()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            return authorizationHeader == StringValues.Empty ? string.Empty : authorizationHeader.Single().Split(" ").Last();
        }

        public string GetUserNameFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var nameClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)
                            ?? jwt.Claims.FirstOrDefault(c => c.Type == "unique_name");

            return nameClaim?.Value;
        }

        public Guid GetUserId => Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);

        public string GetUserName => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

        public string GetName => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "name").Value;
    }
}
