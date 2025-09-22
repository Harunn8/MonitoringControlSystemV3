using ITokenInformationHelper.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ITokenInformationHelper
{
    public class TokenInformationService : ITokenInformationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Guid UserId => throw new NotImplementedException();

        public string UserName => throw new NotImplementedException();

        public bool IsAuthenticated => throw new NotImplementedException();
    }
}
