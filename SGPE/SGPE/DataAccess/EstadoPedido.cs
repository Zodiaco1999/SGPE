using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class EstadoPedido
{
    public long IdEstadoPedido { get; set; }

    public string? DescripcionEstadoPedido { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
