namespace SGPE.DTOS;

public class ModuloDto
{
    public Guid IdModulo { get; set; }
    public string NombreModulo { get; set; } = null!;
    public string DescModulo { get; set; } = null!;
    public string IconoPrefijo { get; set; } = null!;
    public string IconoNombre { get; set; } = null!;
    public short Orden { get; set; }
    public bool? Activo { get; set; }

    public List<MenuDto> Menus { get; set; } = new();
}
