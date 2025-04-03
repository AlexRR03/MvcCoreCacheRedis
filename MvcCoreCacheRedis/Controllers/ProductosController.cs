using Microsoft.AspNetCore.Mvc;
using MvcCoreCacheRedis.Models;
using MvcCoreCacheRedis.Repositories;
using MvcCoreCacheRedis.Services;

namespace MvcCoreCacheRedis.Controllers
{
    public class ProductosController : Controller
    {
        private RepositoryProductos repo;
        private ServiceCacheRedis service;
        public ProductosController( RepositoryProductos repo, ServiceCacheRedis service)
        {
            this.service = service;
            this.repo = repo;
        }

        public async Task<IActionResult> Favoritos()
        {
            List<Producto> favoritos = await this.service.GetProdusctosFavAsync();
            return View(favoritos);
        }

        public async Task<IActionResult> SeleccionarFavorito(int id)
        {
            Producto producto = this.repo.FindProducto(id);
            await this.service.AddProductoFavAsync(producto);
            return RedirectToAction("Favoritos");
        }

        
        public async Task<IActionResult> DeleteFavoritos(int id)
        {
            await this.service.DeleteProductoFavAsync(id);
            return RedirectToAction("Favoritos");
        }

        public IActionResult Index()
        {
            List<Producto> productos = this.repo.GetProductos();
            return View(productos);
        }
        public IActionResult Details(int id)
        {
            Producto producto = this.repo.FindProducto(id);
            return View(producto);
        }
    }
}
