using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class EstadoProducto
{
    public long IdEstadoProducto { get; set; }

    public string DescripcionEstadoProducto { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
