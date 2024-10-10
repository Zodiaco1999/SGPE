using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class Menu
{
    public Guid IdMenu { get; set; }

    public Guid IdModulo { get; set; }

    public string NombreMenu { get; set; } = null!;

    public string EtiquetaMenu { get; set; } = null!;

    public string DescMenu { get; set; } = null!;

    public string Url { get; set; } = null!;

    public short Orden { get; set; }

    public bool Consulta { get; set; }

    public bool Inserta { get; set; }

    public bool Actualiza { get; set; }

    public bool Elimina { get; set; }

    public bool Activa { get; set; }

    public bool Ejecuta { get; set; }

    public bool Activo { get; set; }

    public string CreaUsuario { get; set; } = null!;

    public string CreaMaquina { get; set; } = null!;

    public DateTime CreaFecha { get; set; }

    public string ModificaUsuario { get; set; } = null!;

    public string ModificaMaquina { get; set; } = null!;

    public DateTime ModificaFecha { get; set; }

    public bool Eliminado { get; set; }

    public virtual Modulo IdModuloNavigation { get; set; } = null!;

    public virtual ICollection<PerfilMenu> PerfilMenus { get; set; } = new List<PerfilMenu>();
}
