using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Base
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<bool> LogOutAsync(Guid id);
    }
}
