using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class DetallePedidoEliminado
{
    public long IdDetallePedido { get; set; }

    public long IdPedido { get; set; }

    public long IdProducto { get; set; }

    public int NumeroLinea { get; set; }

    public int Cantidad { get; set; }

    public decimal SubTotal { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime? FechaElim { get; set; }

    public long? UsuarioElim { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
