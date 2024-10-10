namespace SGPE.DTOS;

public class ChangePasswordDto
{
    public string ContrasenaActual { get; set; } = string.Empty;
    public string ContrasenaNueva { get; set; } = null!;
    public string ContrasenaConfirmar { get; set; } = null!;
}
