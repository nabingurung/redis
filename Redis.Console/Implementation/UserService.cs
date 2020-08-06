using Redis.Console.Model;
using System.Collections.Generic;

namespace Redis.Console
{
    public class UserService
    {
        public IEnumerable<User> GetUsers()
        {
            return new User[]
            {
                new User(){Id = 1, FullName="Nabin Gurung",UserId="ngurung"},
                new User(){Id = 2, FullName="John Doe",UserId="JDoe"},
                new User(){Id = 3, FullName="Mary Doe",UserId="MDoe"},
            };
        }
    }
}
