namespace SGPE.DTOS
{
    public class ResetPasswordDto
    {
        public string Correo { get; set; } = string.Empty;
        public string ContrasenaNueva { get; set; } = string.Empty;
    }
}
