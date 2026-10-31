using ProductosApi.DTOs;
using ProductosApi.Models;
using ProductosApi.Repositories;

namespace ProductosApi.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public bool Actualizar(int id, CreateProductoDto dto)
        {
            var producto = _repository.ObtenerPorId(id);

            if (producto == null) {             
                return false;
            }

            producto.Nombre = dto.Nombre;
            producto.Precio = dto.Precio;
            producto.Categoria = dto.Categoria;

            return _repository.Actualizar(producto);
        }

        public ProductoDto Crear(CreateProductoDto dto)
        {
            var producto = new Producto()
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Categoria = dto.Categoria,
                Activo = true,
            };

            _repository.Agregar(producto);
            return ADto(producto);
        }

        public bool Eliminar(int id)
        {
            return _repository.Eliminar(id);
        }

        public ProductoDto? ObtenerPorId(int id)
        {
            var producto = _repository.ObtenerPorId(id);
            return producto == null ? null : ADto(producto);
        }

        public List<ProductoDto> ObtenerTodos()
        {
            return _repository.ObtenerTodos().Select(p => ADto(p)).ToList();
        }

        private static ProductoDto ADto(Producto p) 
        {
            return new ProductoDto { Id = p.Id, Nombre = p.Nombre, Precio = p.Precio, Categoria = p.Categoria, Activo = p.Activo };
        }
    }
}
