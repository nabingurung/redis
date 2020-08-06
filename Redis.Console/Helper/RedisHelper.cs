using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Redis.Console
{
    /// <summary>
    /// A redis helper class. 
    /// used if no dependency is required. 
    /// </summary>
    public class RedisHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> _lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
               return _lazyConnection.Value;
            }
        }
        static RedisHelper()
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(()=> {
                return ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["RedisConnection"]);
            });
        }
    }
}
