using McsCore.Entities;
using McsCore.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.AppDbContext.Mongo
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<UserLogs> UserLogs =>
            _database.GetCollection<UserLogs>("UserLogs");
    }
}
