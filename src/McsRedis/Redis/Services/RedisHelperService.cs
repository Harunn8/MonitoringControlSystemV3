using Redis.Services.Base;
using Redis.Settings;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Redis.Services
{
    public class RedisHelperService : IRedisHelper
    {
        private readonly IDatabase _db;
        private readonly RedisSettings _settings;

        public RedisHelperService(IConnectionMultiplexer redis, RedisSettings settings)
        {
            _db = redis.GetDatabase();
            _settings = settings;
        }

        public async Task<string> GetAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task SetAsync(string key, string value, TimeSpan? expiration = null)
        {
            await _db.StringSetAsync(key, value, expiration);
        }

        public Task<bool> DeleteAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key cannot be null or empty", nameof(key));
            }
            return _db.KeyDeleteAsync(key);
        }
    }
}
