namespace SGPE.DTOS;

public class PerfilEditDto
{
    public Guid IdPerfil { get; set; }
    public string NombrePerfil { get; set; } = null!;
    public string DescPerfil { get; set; } = null!;

    public virtual ICollection<PerfilMenuEditDto> PerfilMenus { get; set; } = new List<PerfilMenuEditDto>();
}
