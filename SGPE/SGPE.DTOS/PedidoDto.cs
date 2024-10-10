namespace SGPE.DTOS;

public class PedidoDto
{
    public Guid IdPedido { get; set; }

    public Guid IdUsuarioSolicita { get; set; }

    public decimal ValorTotal { get; set; }

    public DateTime FechaSolicita { get; set; }

    public virtual string? EstadoPedido { get; set; }

    public virtual string? Usuario { get; set; }

    public virtual string? CedulaUsuario { get; set; }

    public int CantidadProductos { get; set; }
}
