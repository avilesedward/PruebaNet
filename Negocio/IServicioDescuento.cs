using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public interface IServicioDescuento
    {
        public Task<int> ObtenerDescuento(int Id);
        public Task<decimal> CalcularPrecioFinal(decimal Precio, int Descuento);
    }
}
