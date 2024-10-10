namespace SGPE.DTOS;

public class PerfilDto
{
    public Guid IdPerfil { get; set; }
    public string NombrePerfil { get; set; } = null!;
    public string DescPerfil { get; set; } = null!;
    public bool Activo { get; set; }

    public virtual ICollection<PerfilMenuDto> PerfilMenus { get; set; } = new List<PerfilMenuDto>();
}
