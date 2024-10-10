using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class CategoriaProducto
{
    public long IdCategoriaProducto { get; set; }

    public long IdEmpresa { get; set; }

    public bool Activo { get; set; }

    public string CodigoCategoriaProducto { get; set; } = null!;

    public string NombreCategoriaProducto { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
