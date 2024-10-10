namespace SGPE.DTOS
{
    public class LoginResult
    {
        public UsuarioDto Usuario { get; set; } = null!;
        public string Jwt { get; set; } = string.Empty;
        public string TokenSession { get; set; } = string.Empty;
    }
}
