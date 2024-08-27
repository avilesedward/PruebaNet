using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Negocio
{
    public class ServicioEstado: IServicioEstado
    {
        private readonly IMemoryCache cache;
        private static Dictionary<int, string> estados = new Dictionary<int, string>();
        public IConfiguration configuration;
        public ServicioEstado(IMemoryCache _cache, IConfiguration _configuration)
        {
            configuration = _configuration;
            cache = _cache;
            if (!estados.ContainsKey(0)) estados.Add(0, "Inactive");
            if (!estados.ContainsKey(1)) estados.Add(1, "Active");
        }

        public async Task<IDictionary<int, string>> GetEstados()
        {

            return await ObtenerCache(
                "estados",
                async () =>
                {
                    var result = await Task.FromResult(estados);
                    if (result != null)
                        return result;
                    throw new Exception("Error al cargar Datos");
                });
        }

        public async Task<T> ObtenerCache<T>(string cacheKey,Func<Task<T>> funcionData)
        {
            if (!cache.TryGetValue(cacheKey, out T Datos))
            {
                Datos = await funcionData();
                double TiempoMinutosCache = 5;
                double.TryParse(configuration["TiempoMinutosCache"], out TiempoMinutosCache);

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TiempoMinutosCache==0? TimeSpan.FromMinutes(5): TimeSpan.FromMinutes(TiempoMinutosCache)
                };
                cache.Set(cacheKey, Datos, cacheEntryOptions);
            }
            return Datos;
        }
    }
}
