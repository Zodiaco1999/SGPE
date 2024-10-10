namespace SGPE.DTOS;

public class ProductoPedidoDto
{
    public long IdProducto { get; set; }

    public long IdCategoriaProducto { get; set; }

    public long IdEmpresa { get; set; }

    public long IdEstadoProducto { get; set; }

    public int CodigoErp { get; set; }

    public string DescripcionProducto { get; set; } = null!;

    public decimal Precio { get; set; }

    public string? ImagenBase64 { get; set; }

    public int? Cantidad { get; set; }
}
