using System;
using System.Collections.Generic;

namespace SGPE.WebApi.DataAccess;

public partial class Producto
{
    public long IdProducto { get; set; }

    public long IdCategoriaProducto { get; set; }

    public long IdEmpresa { get; set; }

    public long IdEstadoProducto { get; set; }

    public int CodigoErp { get; set; }

    public string DescripcionProducto { get; set; } = null!;

    public decimal Precio { get; set; }

    public string? ImagenBase64 { get; set; }

    public int OrdenVisualizacion { get; set; }

    public string? CreaUsuario { get; set; }

    public string? CreaMaquina { get; set; }

    public DateTime? CreaFecha { get; set; }

    public string? ModificaUsuario { get; set; }

    public string? ModificaMaquina { get; set; }

    public DateTime? ModificaFecha { get; set; }

    public bool Eliminado { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual CategoriaProducto IdCategoriaProductoNavigation { get; set; } = null!;

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    public virtual EstadoProducto IdEstadoProductoNavigation { get; set; } = null!;
}
