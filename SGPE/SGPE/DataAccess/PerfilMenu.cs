using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class PerfilMenu
{
    public Guid IdPerfil { get; set; }

    public Guid IdMenu { get; set; }

    public Guid IdModulo { get; set; }

    public bool Consulta { get; set; }

    public bool Inserta { get; set; }

    public bool Actualiza { get; set; }

    public bool Elimina { get; set; }

    public bool Activa { get; set; }

    public bool Ejecuta { get; set; }

    public virtual Menu IdM { get; set; } = null!;

    public virtual Modulo IdModuloNavigation { get; set; } = null!;

    public virtual Perfil IdPerfilNavigation { get; set; } = null!;
}
