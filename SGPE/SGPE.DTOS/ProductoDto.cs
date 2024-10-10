namespace SGPE.DTOS
{
    public class ProductoDto
    {
        public long IdProducto { get; set; }
        public long IdCategoriaProducto { get; set; }
        public long IdEmpresa { get; set; }
        public long IdEstadoProducto { get; set; }
        public long? IdUsuarioModifica { get; set; }
        public int CodigoErp { get; set; }
        public string? DescripcionProducto { get; set; }
        public decimal Precio { get; set; }
        public string? ImagenBase64 { get; set; }
        public int OrdenVisualizacion { get; set; }
        public DateTime? CreaFecha { get; set; }
        public DateTime? ModificaFecha { get; set; }

        public virtual string? CategoriaProducto { get; set; }
        public virtual string? NombreEmpresa { get; set; }
        public virtual string? EstadoProducto { get; set; }
    }
}
