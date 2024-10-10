namespace SGPE.WebApi.DataAccess;

public partial class Pedido
{
    public Guid IdPedido { get; set; }

    public Guid IdUsuarioSolicita { get; set; }

    public long IdEstadoPedido { get; set; }

    public Guid? IdUsuarioDescarga { get; set; }

    /// <summary>
    /// Este valor total es con IVA incluido, aun asi es valor aproximado, quien liquida es el SIC
    /// </summary>
    public decimal ValorTotal { get; set; }

    public DateTime? FechaPedidoDescarga { get; set; }

    public string CreaUsuario { get; set; } = null!;

    public string CreaMaquina { get; set; } = null!;

    public DateTime CreaFecha { get; set; }

    public string? ModificaUsuario { get; set; }

    public string? ModificaMaquina { get; set; }

    public DateTime? ModificaFecha { get; set; }

    public bool Eliminado { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual EstadoPedido IdEstadoPedidoNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioDescargaNavigation { get; set; }

    public virtual Usuario IdUsuarioSolicitaNavigation { get; set; } = null!;
}
