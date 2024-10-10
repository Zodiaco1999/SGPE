namespace SGPE.DTOSM
{
    public class UsuarioPerfilDto
    {
        public Guid IdUsuario { get; set; }
        public Guid IdPerfil { get; set; }
        public string NombrePerfil { get; set; } = string.Empty;
        public DateTime? FechaInicia { get; set; }
        public DateTime? FechaTermina { get; set; }
    }
}
