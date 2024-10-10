using SGPE.DTOSM;

namespace SGPE.DTOS
{
    public class UsuarioEditDto
    {
        public Guid IdUsuario { get; set; }
        public long? IdEmpresa { get; set; }
        public string CedulaUsuario { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public bool Activo { get; set; }
        public string? NombreEmpresa { get; set; }

        public ICollection<UsuarioPerfilDto> UsuarioPerfils { get; set; } = new List<UsuarioPerfilDto>();
    }
}
