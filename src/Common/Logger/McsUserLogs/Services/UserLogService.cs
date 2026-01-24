using AutoMapper;
using McsCore.AppDbContext;
using McsCore.AppDbContext.Mongo;
using McsCore.Entities;
using McsUserLogs.Responses;
using McsUserLogs.Services.Base;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsUserLogs.Services
{
    public class UserLogService : IUserLogService
    {
        private readonly IMongoCollection<UserLogs> _userLogs;
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserLogService(MongoDbContext context, McsAppDbContext dbContext, IMapper mapper, IMongoDatabase userLogsDatabase)
        {
            _userLogs = context.UserLogs;
            _dbContext = dbContext;
            _mapper = mapper;
            _userLogs = userLogsDatabase.GetCollection<UserLogs>("UserLogs");
        }

        public async Task SetEventUserLog(UserLogs log)
        {
            // Güncel kullanıcının bilgisini alınarak userModel içerisine yazdırılmalı
            // TODO : TokenInformation düzeltilerek güncel kullanıcı adı alınmalı

            try
            {
                await _userLogs.InsertOneAsync(log);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public async Task<List<McsUserLogResponse>> GetUserLogByUserId(Guid userId)
        {
           throw new NotImplementedException();
        }
    }
}
