using DTO;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Negocio
{
    public class ServicioDescuento: IServicioDescuento
    {
        public HttpClient _httpClient = new HttpClient();
        public IConfiguration _configuration;

        public ServicioDescuento(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["urlDescuentos"]);
        }

        public async Task<int> ObtenerDescuento(int Id)
        {
            int resultado = 0;
            try
            {
                var response = await _httpClient.GetAsync("api/v1/Discount/" + Id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var descuento = await response.Content.ReadAsAsync<DescuentoDto>();
                    if (descuento != null)
                    {
                        resultado = descuento.Value;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return resultado;
        }

        public async Task<decimal> CalcularPrecioFinal(decimal Precio, int Descuento)
        {
            decimal resultado = Precio;
            try
            {
                resultado = Precio * (100 - Descuento) / 100;
            }
            catch (Exception ex)
            {

            }
            return resultado;
        }
    }
}
