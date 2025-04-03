using StackExchange.Redis;

namespace MvcCoreCacheRedis.Helpers
{
    public static class HelperCacheMultiplexer
    {
        private static string cacheRedisKeys;

        public static void Initialize(IConfiguration configuration)
        {
            cacheRedisKeys = configuration["AzureKeys:CacheRedis"];
        }

        public static Lazy<ConnectionMultiplexer>
            CreateConnection = new Lazy<ConnectionMultiplexer>(  () =>
            {

                

                return  ConnectionMultiplexer.Connect(cacheRedisKeys);
            });  

        public static ConnectionMultiplexer Connection
        {
            get { return CreateConnection.Value; }
        }

        
    }
}
