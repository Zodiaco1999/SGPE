namespace SGPE.DTOS;

public class PerfilMenuDto
{
    public Guid IdPerfil { get; set; }
    public Guid IdModulo { get; set; }
    public Guid IdMenu { get; set; }
    public string DescMenu { get; set; } = string.Empty;
    public bool Consulta { get; set; }
    public bool Inserta { get; set; }
    public bool Actualiza { get; set; }
    public bool Elimina { get; set; }
    public bool Activa { get; set; }
    public bool Ejecuta { get; set; }

    public bool MenuConsulta { get; set; }
    public bool MenuInserta { get; set; }
    public bool MenuActualiza { get; set; }
    public bool MenuElimina { get; set; }
    public bool MenuActiva { get; set; }
    public bool MenuEjecuta { get; set; }

    public string NombreMenu { get; set; } = string.Empty;
    public string NombreModulo { get; set; } = string.Empty;    

}