using StackExchange.Redis;

namespace ConsoleCacheRedis
{
    public static class HelperCacheMultiplexer
    {
        public static Lazy<ConnectionMultiplexer>
            CreateConnection = new Lazy<ConnectionMultiplexer>(  () =>
            {
                //Necesitamos Obtener la Key desde el appsettings.json pero al ser static no se pude inyectar IConfiguration
                //BUSCAR OTRA FORMA DE HACERLO
                string cacheRedisKeys = "";



                return  ConnectionMultiplexer.Connect(cacheRedisKeys);
            });  

        public static ConnectionMultiplexer Connection
        {
            get { return CreateConnection.Value; }
        }


    }
}
