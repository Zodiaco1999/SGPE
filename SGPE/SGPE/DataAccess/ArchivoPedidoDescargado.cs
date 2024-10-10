using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class ArchivoPedidoDescargado
{
    public long IdArchivoPedidoDescargado { get; set; }

    public DateTime FechaDescarga { get; set; }

    public long IdUsuarioDescarga { get; set; }

    public string ContenidoArchivo { get; set; } = null!;

    public int CantidadPedidosDescargados { get; set; }

    public DateTime FechaRegistro { get; set; }
}
