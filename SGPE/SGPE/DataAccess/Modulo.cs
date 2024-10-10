using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class Modulo
{
    public Guid IdModulo { get; set; }

    public string NombreModulo { get; set; } = null!;

    public string DescModulo { get; set; } = null!;

    public string IconoPrefijo { get; set; } = null!;

    public string IconoNombre { get; set; } = null!;

    public short Orden { get; set; }

    public bool Activo { get; set; }

    public string CreaUsuario { get; set; } = null!;

    public string CreaMaquina { get; set; } = null!;

    public DateTime CreaFecha { get; set; }

    public string ModificaUsuario { get; set; } = null!;

    public string ModificaMaquina { get; set; } = null!;

    public DateTime ModificaFecha { get; set; }

    public bool Eliminado { get; set; }

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<PerfilMenu> PerfilMenus { get; set; } = new List<PerfilMenu>();
}
