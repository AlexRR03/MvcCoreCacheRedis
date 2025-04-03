using ConsoleCacheRedis;
Console.WriteLine("Testing Cache Redis");
Console.ReadLine();
ServiceCacheRedis service = new ServiceCacheRedis();
List<Producto> favoritos = await service.GetProdusctosFavAsync();
if (favoritos == null)
{
    Console.WriteLine("No hay productos favoritos");
}
else
{
    int i = 1;
    foreach (Producto p in favoritos)
    {
        Console.WriteLine(i + ".- " + p.Nombre);
        i++;
    }
}
Console.ReadLine();
 