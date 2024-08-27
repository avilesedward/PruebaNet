using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Negocio;

namespace Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoNegocio productoNegocio;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_productoNegocio"></param>
        public ProductoController(IProductoNegocio _productoNegocio)
        {
            productoNegocio = _productoNegocio;
        }

        [HttpGet("Health")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult> Health()
        {
            return Ok("OK");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType<IEnumerable<ProductoDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
        {
            try
            {
                var result = await productoNegocio.ListarTodo();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType<IEnumerable<ProductoDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoDto>> Get(long id)
        {
            ProductoDto resultado= new ProductoDto();
            try
            {
                if (id <= 0)
                    return BadRequest();
                
                resultado = await productoNegocio.ObtenerPorId(id);

                if(resultado.ProductId == 0)
                    return NotFound();

                return Ok(resultado);

            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType<IEnumerable<ProductoDto>>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoDto>> Post([FromBody] ProductoDto value)
        {
            ProductoDto resultado = new ProductoDto();
            try
            {
                if (string.IsNullOrEmpty(value.Name) || value.Price <= 0)
                    return BadRequest(value);

                resultado = await productoNegocio.CrearProducto(value);

                if (resultado.ProductId == 0)
                    return Conflict(value);

                return Created(string.Empty, resultado);

            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType<IEnumerable<ProductoDto>>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoDto>> Put(long id, [FromBody] ProductoDto value)
        {
            ProductoDto resultado = new ProductoDto();
            try
            {
                if (value.ProductId <= 0 || string.IsNullOrEmpty(value.Name) || value.Price <= 0)
                    return BadRequest(value);

                resultado = await productoNegocio.ActualizarProducto(id, value);
                if (resultado == null)
                    return NotFound(value);

                if (resultado.ProductId == 0)
                    return Conflict(value);

                return Created(string.Empty, resultado);

            }
            catch (Exception)
            {
                return StatusCode(500, value);
            }
        }

    }
}
