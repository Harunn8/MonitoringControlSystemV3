using System;
using TokenInformation.Base;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
namespace TokenInformation
{
    public class TokenInformationService : ITokenInformationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenInformationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;
        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;


        public Guid UserId
        {
            get
            {
                var idStr = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? User?.FindFirst("sub")?.Value;

                return Guid.TryParse(idStr, out var gid) ? gid : Guid.Empty;
            }
        }

        public string UserName =>
       User?.FindFirst(ClaimTypes.Name)?.Value
       ?? User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

    }
}
