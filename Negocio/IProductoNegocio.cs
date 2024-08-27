using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public interface IProductoNegocio
    {
        public Task<List<ProductoDto>> ListarTodo();
        public Task<ProductoDto> ObtenerPorId(long Id);
        public Task<ProductoDto> CrearProducto(ProductoDto productoDto);

        public Task<ProductoDto> ActualizarProducto(long Id,ProductoDto productoDto);
    }
}
