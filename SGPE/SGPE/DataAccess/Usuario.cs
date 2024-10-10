using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class Usuario
{
    public Guid IdUsuario { get; set; }

    public long? IdEmpresa { get; set; }

    public string CedulaUsuario { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Correo { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public bool Activo { get; set; }

    public string? Token { get; set; }

    public DateTime? FechaExpiracionToken { get; set; }

    public string CreaUsuario { get; set; } = null!;

    public string CreaMaquina { get; set; } = null!;

    public DateTime CreaFecha { get; set; }

    public string? ModificaUsuario { get; set; }

    public string? ModificaMaquina { get; set; }

    public DateTime? ModificaFecha { get; set; }

    public bool Eliminado { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual ICollection<Pedido> PedidoIdUsuarioDescargaNavigations { get; set; } = new List<Pedido>();

    public virtual ICollection<Pedido> PedidoIdUsuarioSolicitaNavigations { get; set; } = new List<Pedido>();

    public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; set; } = new List<UsuarioPerfil>();
}
