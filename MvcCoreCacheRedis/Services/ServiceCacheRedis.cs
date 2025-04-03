using MvcCoreCacheRedis.Helpers;
using MvcCoreCacheRedis.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MvcCoreCacheRedis.Services
{
    public class ServiceCacheRedis
    {
        private IDatabase database;

        public ServiceCacheRedis() { 
            this.database = HelperCacheMultiplexer.Connection.GetDatabase();
        }

        public async Task AddProductoFavAsync(Producto producto)
        {
            string jsonProductos = await
                this.database.StringGetAsync("favoritos");
            List<Producto> productos;
            if(jsonProductos == null)
            {
                productos = new List<Producto>();
            }
            else
            {
                productos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
            }
            productos.Add(producto);
            jsonProductos = JsonConvert.SerializeObject(productos);
            await this.database.StringSetAsync("favoritos", jsonProductos, TimeSpan.FromMinutes(30));
        }

        public async Task<List<Producto>> GetProdusctosFavAsync()
        {
            string jsonProductos = await this.database.StringGetAsync("favoritos");
            if (jsonProductos == null)
            {
                return null;
            }
            else
            {
                List<Producto> productos = JsonConvert.DeserializeObject<List<Producto>>(jsonProductos);
                return productos;
            }

        }
        public async Task DeleteProductoFavAsync(int idProducto)
        {
            List<Producto> favoritos = await this.GetProdusctosFavAsync();
            if (favoritos != null)
            {
                Producto productoDelete = favoritos.FirstOrDefault(p => p.IdProducto == idProducto);
                favoritos.Remove(productoDelete);
                if (favoritos.Count == 0)
                {
                    await this.database.KeyDeleteAsync("favoritos");
                }
                else
                {
                    string jsonProductos = JsonConvert.SerializeObject(favoritos);
                    await this.database.StringSetAsync("favoritos", jsonProductos, TimeSpan.FromMinutes(30));
                }
            }
        }

    }
}
