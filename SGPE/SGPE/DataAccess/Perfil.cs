using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class Perfil
{
    public Guid IdPerfil { get; set; }

    public string NombrePerfil { get; set; } = null!;

    public string DescPerfil { get; set; } = null!;

    public bool Activo { get; set; }

    public string CreaUsuario { get; set; } = null!;

    public string CreaMaquina { get; set; } = null!;

    public DateTime CreaFecha { get; set; }

    public string ModificaUsuario { get; set; } = null!;

    public string ModificaMaquina { get; set; } = null!;

    public DateTime ModificaFecha { get; set; }

    public bool Eliminado { get; set; }

    public virtual ICollection<PerfilMenu> PerfilMenus { get; set; } = new List<PerfilMenu>();

    public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; set; } = new List<UsuarioPerfil>();
}
