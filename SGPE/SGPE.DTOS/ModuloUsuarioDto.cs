namespace SGPE.DTOS;

public class ModuloUsuarioDto
{
    public Guid IdModulo { get; set; }
    public string NombreModulo { get; set; } = null!;
    public string DescModulo { get; set; } = null!;
    public string IconoPrefijo { get; set; } = null!;
    public string IconoNombre { get; set; } = null!;
    public IEnumerable<MenuUsuarioDto> MenusUsuario { get; set; } = Enumerable.Empty<MenuUsuarioDto>();
}
