using UserApplication.Models;
using UserApplication.Services.Base;
using AutoMapper;
using McsCore.AppDbContext;
using McsCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApplication.Services
{
    public class UserService : IUserService
    {
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserService(McsAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UsersModel>> GetAllUserAsync()
        {
            var entites = await _dbContext.Users.ToListAsync();
            var users = _mapper.Map<List<UsersModel>>(entites);
            return new List<UsersModel>(users);
        }

        public async Task<UsersModel> GetUserByIdAsync(Guid id)
        {
            var entity = _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                var user = _mapper.Map<UsersModel>(entity);
                return user;
            }
            return null;
        }

        public async Task<UsersModel> GetUserByName(string userName)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if(entity != null)
            {
                var user = _mapper.Map<UsersModel>(entity);
                return user;
            }
            return null;
        }

        public async Task<UsersModel> CreateUser(Users userModel)
        {

            var user = new Users()
            {
                Id = Guid.NewGuid(),
                UserName = userModel.UserName,
                Password = userModel.Password,
                CreateDate = DateTime.Now
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            var response = _mapper.Map<UsersModel>(user);
            return response;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
              _dbContext.Users.Remove(user);
              _dbContext.SaveChanges();
        }

        public async Task UpdateUser(Guid id, Users userModel)
        {
            var user  = await GetUserByIdAsync(id);
            _dbContext.Update(userModel);
            await _dbContext.SaveChangesAsync();
        }
    }
}
