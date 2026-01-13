using UserApplication.Models;
using UserApplication.Services.Base;
using AutoMapper;
using McsCore.AppDbContext;
using McsCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using Aes = System.Security.Cryptography.Aes;
using System.IO;

namespace UserApplication.Services
{
    public class UserService : IUserService
    {
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private static readonly string key = "BoQxCC547889_!uYzERtwqX25PerQ@%/";
        private static readonly string IV = "Xert%7548qxYvBNa";
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
            if (entity != null)
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
                Password = Encrypt(userModel.Password),
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
            var user = await GetUserByIdAsync(id);
            _dbContext.Update(userModel);
            await _dbContext.SaveChangesAsync();
        }

        public string Encrypt(string hashingPassword)
        {
            byte[] keys = Encoding.UTF8.GetBytes(key);
            byte[] iv = Encoding.UTF8.GetBytes(IV);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keys;
                aes.IV = iv;

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (var writer = new StreamWriter(cryptoStream, Encoding.UTF8, 1024, leaveOpen: true))
                        {
                            writer.Write(hashingPassword);
                        }
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        public string Decrypt(string encryptedText)
        {
            byte[] keys = Encoding.UTF8.GetBytes(key);
            byte[] iv = Encoding.UTF8.GetBytes(IV);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keys;
                aes.IV = iv;

                using (var memoryStream = new MemoryStream(Convert.FromBase64String(encryptedText)))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cryptoStream))
                        {
                            var aa = reader.ReadToEnd();
                            return aa;
                        }
                    }
                }
            }
        }
    }
}
