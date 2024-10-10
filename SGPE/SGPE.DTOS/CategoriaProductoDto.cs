namespace SGPE.DTOS;

public class CategoriaProductoDto
{
    public long IdCategoriaProducto { get; set; }
    public long IdEmpresa { get; set; }
    public string CodigoCategoriaProducto { get; set; } = string.Empty;
    public string NombreCategoriaProducto { get; set; } = string.Empty;
    public string NombreEmpresa { get; set; } = string.Empty;
    public bool? Activo { get; set; }
}
