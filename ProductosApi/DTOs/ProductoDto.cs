namespace ProductosApi.DTOs
{
    public class ProductoDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public string Categoria { get; set; }

        public bool Activo { get; set; }
    }
}
