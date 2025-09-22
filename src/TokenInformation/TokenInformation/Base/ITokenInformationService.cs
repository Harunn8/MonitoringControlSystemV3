using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenInformation.Base
{
    public interface ITokenInformationService
    {
        Guid UserId { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
    }
}
