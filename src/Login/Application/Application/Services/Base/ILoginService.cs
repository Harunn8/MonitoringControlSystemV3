using LoginApplication.Models;
using LoginApplication.Responses;
using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApplication.Models;

namespace LoginApplication.Services.Base
{
    public interface ILoginService
    {
        Task<bool> ValidateUser(string userName , string password);
        Task<bool> LogOutAsync(Guid id);
        string GenerateJwtToken(string userName);
        Task<LoginResponses> Login(LoginRequest request);
    }
}
