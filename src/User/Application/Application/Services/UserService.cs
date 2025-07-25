using Application.Models;
using Application.Services.Base;
using McsCore.AppDbContext;
using McsCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly McsAppDbContext _dbContext;
        public UserService(McsAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UsersModel>> GetAllUserAsync()
        {
           //var users = await _dbContext.Users.ToListAsync();
           // return new List<UsersModel>(users)
        }

        public Task<UsersModel> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UsersModel> GetUserByName(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<UsersModel> CreateUser(Users userModel)
        {
            await _dbContext.Users.AddAsync(userModel);
            return new UsersModel
            {
                Id = Guid.NewGuid(),
                UserName = userModel.UserName,
                Password = userModel.Password,
                CreateDate = DateTime.Now
            };
        }

        public Task DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        

        public Task UpdateUser(Guid id, Users userModel)
        {
            throw new NotImplementedException();
        }
    }
}
