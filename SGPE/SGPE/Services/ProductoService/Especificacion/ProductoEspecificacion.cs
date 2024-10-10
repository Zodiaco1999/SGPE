using SGPE.Comun.EspecificacionBase;

namespace SGPE.WebApi.Services.ProductoService.Especificacion;

public class ProductoEspecificacion : SpecificationBase<Producto>
{
    public ProductoEspecificacion(string textoBusqueda)
    {
        Criteria = BusquedaTextoCompleto(textoBusqueda).SatisfiedBy();
    }

    private ISpecificationCriteria<Producto> BusquedaTextoCompleto(string texto)
    {
        SpecificationCriteria<Producto> especificacion = new SpecificationCriteriaTrue<Producto>();
        var spl = texto.ToLower().Trim().Split(' ');

        foreach (var s in spl)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                var eEspecificacion1 = new SpecificationCriteriaDirect<Producto>(
                    p => p.CodigoErp.ToString().StartsWith(s)
                    || p.DescripcionProducto.Contains(s)
                    || p.Precio.ToString().StartsWith(s)
                    || p.IdCategoriaProductoNavigation.NombreCategoriaProducto.Contains(s)
                    || p.IdEstadoProductoNavigation.DescripcionEstadoProducto.StartsWith(s));

                especificacion &= eEspecificacion1;
            }
        }

        return especificacion;
    }
}
