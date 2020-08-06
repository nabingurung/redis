using Redis.Console.Abstraction;
using System;
using System.Configuration;

namespace Redis.Console
{
    public class Settings : ISettings
    {
        public double CacheTimeout { get; }

        public Settings()
        {
            CacheTimeout = Convert.ToDouble(ConfigurationManager.AppSettings["CacheTimeOut"]);
        }

    }
}
