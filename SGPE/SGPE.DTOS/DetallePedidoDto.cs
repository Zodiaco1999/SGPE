namespace SGPE.DTOS;

public class DetallePedidoDto
{
    public Guid IdDetallePedido { get; set; }
    public Guid IdPedido { get; set; }
    public long IdProducto { get; set; }
    public string CodigoErp { get; set; } = string.Empty;
    public string DescripcionProducto { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal SubTotal { get; set; }
}
