using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Redis.Console.Abstraction;
using Redis.Console.Implementation;
using System;

namespace Redis.Console
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {

            //user cache key 
            var userCacheKey = "Testusers";

            //Register services 
            RegisterServices();

            //create a scope 
            IServiceScope scope = _serviceProvider.CreateScope();    
            
            // create a service 
            var service =    scope.ServiceProvider.GetRequiredService<RedisCacheService>();

            // check for the user cache key. first time it should be false
            if (!service.Exists(userCacheKey))
            {
                var users = new UserService().GetUsers();
                var serializeUsers = JsonConvert.SerializeObject(users);

                service.Save(userCacheKey, serializeUsers);
            }

            // expected value to true. since we just added 
            System.Console.WriteLine(service.Exists(userCacheKey));

            // dispose services 
            DisposeServices();

            //read it from cache in the future


        }

        /// <summary>
        /// Register Services 
        /// </summary>
        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ISettings, Settings>();
            services.AddSingleton<RedisCacheService>();
            _serviceProvider = services.BuildServiceProvider(true);
        }

        /// <summary>
        /// Dispose Services
        /// </summary>

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
