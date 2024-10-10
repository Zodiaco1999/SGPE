namespace SGPE.DTOS;

public class MenuDto
{
    public Guid IdMenu { get; set; }
    public Guid IdModulo { get; set; }
    public string NombreMenu { get; set; } = string.Empty;
    public string EtiquetaMenu { get; set; } = string.Empty;
    public string DescMenu { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool Visible { get; set; }
    public short Orden { get; set; }
    public bool Consulta { get; set; }
    public bool Inserta { get; set; }
    public bool Actualiza { get; set; }
    public bool Elimina { get; set; }
    public bool Activa { get; set; }
    public bool Ejecuta { get; set; }
    public bool Activo { get; set; }
}
