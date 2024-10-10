using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class DetallePedido
{
    public Guid IdDetallePedido { get; set; }

    public Guid IdPedido { get; set; }

    public long IdProducto { get; set; }

    public int Cantidad { get; set; }

    /// <summary>
    /// Este es valor aproximado, ya que el valor lo calcula el SIC.
    /// </summary>
    public decimal SubTotal { get; set; }

    public string CreaUsuario { get; set; } = null!;

    public string CreaMaquina { get; set; } = null!;

    public DateTime CreaFecha { get; set; }

    public string? ModificaUsuario { get; set; }

    public string? ModificaMaquina { get; set; }

    public DateTime? ModificaFecha { get; set; }

    public bool Eliminado { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
