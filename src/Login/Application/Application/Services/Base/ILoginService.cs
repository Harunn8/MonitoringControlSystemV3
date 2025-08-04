using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApplication.Services.Base
{
    public interface ILoginService
    {
        Task<bool> ValidateUser(string userName , string password);
        Task<bool> LogOutAsync(Guid id);
        string GenerateJwtToken(string userName);
    }
}
