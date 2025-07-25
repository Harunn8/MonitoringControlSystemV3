using Application.Models;
using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Base
{
    public interface IUserService
    {
        Task<List<UsersModel>> GetAllUserAsync();
        Task<UsersModel> GetUserByIdAsync(Guid id);
        Task<UsersModel> GetUserByName(string userName);
        Task<UsersModel> CreateUser(Users userModel);
        Task UpdateUser(Guid id, Users userModel);
        Task DeleteUser(Guid id);

    }
}
