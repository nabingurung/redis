using Redis.Console.Abstraction;
using StackExchange.Redis;
using System;
using System.Configuration;

namespace Redis.Console.Implementation
{
    public class RedisCacheService : ICacheService
    {
        private readonly ISettings _settings;
        private readonly IDatabase _cache;
        private static ConnectionMultiplexer _connectionMultiplexer;

        static RedisCacheService()
        {
            var connection = ConfigurationManager.AppSettings["RedisConnection"];
            _connectionMultiplexer = ConnectionMultiplexer.Connect(connection);
        }

        public RedisCacheService(ISettings settings)
        {
            _settings = settings;
            _cache = _connectionMultiplexer.GetDatabase();
        }

        public bool Exists(string key)
        {
            return _cache.KeyExists(key);
        }

        public void Save(string key, string value)
        {
            var ts = TimeSpan.FromMinutes(_settings.CacheTimeout);
            _cache.StringSet(key, value, ts);
        }

        public string Get(string key)
        {
            return _cache.StringGet(key);
        }

        public void Remove(string key)
        {
            _cache.KeyDelete(key);
        }

        public void Purge()
        {
            var endpoints = _connectionMultiplexer.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _connectionMultiplexer.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }
    }
}
