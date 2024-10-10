using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class Empresa
{
    public long IdEmpresa { get; set; }

    public string Nit { get; set; } = null!;

    public string NombreEmpresa { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<CategoriaProducto> CategoriaProductos { get; set; } = new List<CategoriaProducto>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
