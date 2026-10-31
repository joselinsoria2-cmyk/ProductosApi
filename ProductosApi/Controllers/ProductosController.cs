using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosApi.DTOs;
using ProductosApi.Services;

namespace ProductosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]                                   
        public IActionResult ObtenerTodos()
        {
            return Ok(_productoService.ObtenerTodos());     
        }

        [HttpGet("{id}")]                            
        public IActionResult ObtenerPorId(int id)
        {
            var producto = _productoService.ObtenerPorId(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);                     
        }

        [HttpPost]                                   
        public IActionResult Crear([FromBody] CreateProductoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var creado = _productoService.Crear(dto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);   
        }

        [HttpPut("{id}")]                            
        public IActionResult Actualizar(int id, [FromBody] CreateProductoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_productoService.Actualizar(id, dto))
            {
                return NotFound();
            }

            return NoContent();                                      
        }

        [HttpDelete("{id}")]                         
        public IActionResult Eliminar(int id)
        {
            if (!_productoService.Eliminar(id))
            {
                return NotFound();
            }     

            return NoContent();                                       
        }
    }
}
