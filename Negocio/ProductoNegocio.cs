using AutoMapper;
using Datos;
using DTO;
using Microsoft.EntityFrameworkCore;
using Modelos;

namespace Negocio
{
    public class ProductoNegocio: IProductoNegocio
    {
        public readonly ProductDBContext dbContext;
        private readonly IMapper mapper;
        private readonly IServicioDescuento servicioDescuento;
        private readonly IServicioEstado servicioEstado;

        public ProductoNegocio(
            ProductDBContext _context
            , IMapper _mapper
            , IServicioDescuento _servicioDescuento
            , IServicioEstado _servicioEstado
        )
        {
            dbContext = _context;
            mapper = _mapper;
            servicioDescuento = _servicioDescuento;
            servicioEstado = _servicioEstado;
        }

        public async Task<List<ProductoDto>> ListarTodo()
        {
            List<ProductoDto> productosDto = new List<ProductoDto>();
            try
            {
                var productos = await dbContext.Productos.ToListAsync();

                if (productos != null)
                {
                    foreach (var producto in productos)
                    {
                        var productoDto = mapper.Map<ProductoDto>(producto);
                        productoDto.Discount = await servicioDescuento.ObtenerDescuento(ObtenerUltimoDigito(productoDto.ProductId));
                        productoDto.FinalPrice = await servicioDescuento.CalcularPrecioFinal(productoDto.Price, productoDto.Discount);
                        productoDto.StatusName = await ObtenerNombreEstado(producto.Status);
                        productosDto.Add(productoDto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productosDto;
        }

        public async Task<ProductoDto> ObtenerPorId(long Id)
        {
            ProductoDto productoDto = new ProductoDto();
            try
            {
                var producto = await dbContext.Productos.FindAsync(Id);

                if (producto != null)
                {
                    productoDto = mapper.Map<ProductoDto>(producto);
                    productoDto.Discount = await servicioDescuento.ObtenerDescuento(ObtenerUltimoDigito(productoDto.ProductId));
                    productoDto.FinalPrice = await servicioDescuento.CalcularPrecioFinal(productoDto.Price, productoDto.Discount);
                    productoDto.StatusName = await ObtenerNombreEstado(producto.Status);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productoDto;
        }

        public async Task<ProductoDto> CrearProducto(ProductoDto productoDto)
        {
            try
            {
                var producto = mapper.Map<Producto>(productoDto);

                if (producto != null)
                {
                    producto.ProductId = 0;
                    producto.FechaCreacion = DateTime.Now;
                    producto.FechaModificacion = DateTime.Now;
                    producto.Status = await ObtenerIdEstado(productoDto.StatusName);
                    await dbContext.Productos.AddAsync(producto);
                    await dbContext.SaveChangesAsync();

                    productoDto.ProductId = producto.ProductId;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productoDto;
        }

        public async Task<ProductoDto> ActualizarProducto(long Id, ProductoDto productoDto)
        {
            try
            {
                var producto = await dbContext.Productos.FindAsync(Id);
                if (producto == null)
                    return null;

                producto.Name = productoDto.Name;
                producto.Description = productoDto.Description;
                producto.Stock = productoDto.Stock;
                producto.Price = productoDto.Price;
                producto.Status = await ObtenerIdEstado(productoDto.StatusName);
                producto.FechaModificacion = DateTime.Now;

                dbContext.Entry(producto).State = EntityState.Modified;

                await dbContext.SaveChangesAsync();

                productoDto.ProductId = producto.ProductId;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productoDto;
        }

        private async Task<string> ObtenerNombreEstado(int EstadoId)
        {
            string estado = "Inactive";
            try
            {
                var estados = await servicioEstado.GetEstados();
                if(estados != null)
                    estado = estados[EstadoId];
            }
            catch (Exception)
            {

            }
            return estado;
        }

        private async Task<int> ObtenerIdEstado(string NombreEstado)
        {
            int IdEstado = 0;
            try
            {
                var estados = await servicioEstado.GetEstados();
                if (estados != null)
                    IdEstado = estados.SingleOrDefault(x => x.Value.Equals(NombreEstado)).Key;
            }
            catch (Exception)
            {

            }
            return IdEstado;
        }

        private int ObtenerUltimoDigito(long ProductId)
        {
            int numero = 0;
            try
            {
                string cadena = ProductId.ToString();
                cadena = cadena.Substring(cadena.Length - 1,1);
                int.TryParse(cadena, out numero);
            }
            catch (Exception)
            {

            }
            return numero; 
        
        } 
    }
}
