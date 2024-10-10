using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class LogAccionUsuario
{
    public long IdLogAccionUsuario { get; set; }

    public long IdRolUsuario { get; set; }

    /// <summary>
    /// CREAR_PEDIDO (Cuando el usuario confirma el pedido) 
    /// DESCARGAR_PEDIDOS (Cuando el rol_administrador descarga pedidos)
    /// 
    /// </summary>
    public string Accion { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }
}
