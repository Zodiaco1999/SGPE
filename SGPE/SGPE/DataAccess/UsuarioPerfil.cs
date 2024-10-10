using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class UsuarioPerfil
{
    public Guid IdUsuario { get; set; }

    public Guid IdPerfil { get; set; }

    public DateTime? FechaInicia { get; set; }

    public DateTime? FechaTermina { get; set; }

    public virtual Perfil IdPerfilNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
