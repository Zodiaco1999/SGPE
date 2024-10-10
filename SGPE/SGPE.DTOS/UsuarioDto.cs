namespace SGPE.DTOS;

public class UsuarioDto
{
    public Guid IdUsuario { get; set; }
    public long? IdEmpresa { get; set; }
    public string CedulaUsuario { get; set; } = null!;
    public string Nombres { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public bool? Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }

    public string NombreEmpresa { get; set; } = string.Empty;
}
