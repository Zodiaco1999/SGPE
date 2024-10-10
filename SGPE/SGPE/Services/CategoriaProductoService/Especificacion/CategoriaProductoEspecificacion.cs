using SGPE.Comun.EspecificacionBase;

namespace SGPE.WebApi.Services.CategoriaProductoService.Especificacion;

public class CategoriaProductoEspecificacion : SpecificationBase<CategoriaProducto>
{
    public CategoriaProductoEspecificacion(string textoBusqueda)
    {
        Criteria = BusquedaTextoCompleto(textoBusqueda).SatisfiedBy();
    }

    private ISpecificationCriteria<CategoriaProducto> BusquedaTextoCompleto(string texto)
    {
        SpecificationCriteria<CategoriaProducto> especificacion = new SpecificationCriteriaTrue<CategoriaProducto>();
        var spl = texto.ToLower().Trim().Split(' ');

        foreach (var s in spl)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                var eEspecificacion1 = new SpecificationCriteriaDirect<CategoriaProducto>(
                    p => p.NombreCategoriaProducto.Contains(s)
                    || p.CodigoCategoriaProducto.Contains(s)
                    || p.IdEmpresaNavigation.NombreEmpresa.Contains(s));

                especificacion &= eEspecificacion1;
            }
        }

        return especificacion;
    }
}
