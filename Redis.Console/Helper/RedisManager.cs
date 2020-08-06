using Newtonsoft.Json;
using Redis.Console.Model;
using System.Collections.Generic;

namespace Redis.Console
{
    /// <summary>
    /// A redis Manager class. Used it with RedisHelper
    /// </summary>
    public class RedisManager
    {
        public static void SaveDataToRedis()
        {
           
            var cache = RedisHelper.Connection.GetDatabase();
            var service = new UserService();
            var users = service.GetUsers();

            var serializeUsers= JsonConvert.SerializeObject(users);
            var userCacheKey = "Testusers";
            cache.StringSet(userCacheKey, serializeUsers);
        }

        public static void ReadDataFromRedis()
        {
            var cache = RedisHelper.Connection.GetDatabase();
            var serializeUsers = cache.StringGet("Testusers");
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(serializeUsers);
            foreach (var user in users)
            {
                System.Console.WriteLine(string.Format("Id =  {0} , FullName = {1}, UserID = {2} ",user.Id,user.FullName,user.UserId));
            }
        }

        public static void RemoveDataFromRedis()
        {
            var cache = RedisHelper.Connection.GetDatabase();
            cache.KeyDelete("Testusers");
        }
    }
}
