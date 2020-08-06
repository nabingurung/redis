namespace Redis.Console
{
     interface ICacheService
    {
        bool Exists(string key);
        void Save(string key, string value);
        string Get(string key);
        void Remove(string key);
        void Purge();
    }
}