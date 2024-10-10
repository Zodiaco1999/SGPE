namespace SGPE.DTOS;

public class ProductoEditDto
{
    public long IdProducto { get; set; }
    public long IdCategoriaProducto { get; set; }
    public long IdEmpresa { get; set; }
    public long IdEstadoProducto { get; set; }
    public long? IdUsuarioModifica { get; set; }
    public int CodigoErp { get; set; }
    public string DescripcionProducto { get; set; } = null!; 
    public decimal Precio { get; set; }
    public string? ImagenBase64 { get; set; }
    public int OrdenVisualizacion { get; set; }
}
