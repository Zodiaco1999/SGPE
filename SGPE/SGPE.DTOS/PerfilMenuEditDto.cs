namespace SGPE.DTOS;

public class PerfilMenuEditDto
{
    public Guid IdPerfil { get; set; }
    public Guid IdModulo { get; set; }
    public Guid IdMenu { get; set; }
    public bool Consulta { get; set; }
    public bool Inserta { get; set; }
    public bool Actualiza { get; set; }
    public bool Elimina { get; set; }
    public bool Activa { get; set; }
    public bool Ejecuta { get; set; }
}