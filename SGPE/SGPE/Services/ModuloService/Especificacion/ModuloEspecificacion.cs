using SGPE.Comun.EspecificacionBase;

namespace SGPE.WebApi.Services.ModuloService.Especificacion;

public class ModuloEspecificacion : SpecificationBase<Modulo>
{
    public ModuloEspecificacion(string textoBusqueda)
    {
        Criteria = BusquedaTextoCompleto(textoBusqueda).SatisfiedBy();
    }

    private ISpecificationCriteria<Modulo> BusquedaTextoCompleto(string texto)
    {
        SpecificationCriteria<Modulo> especificacion = new SpecificationCriteriaTrue<Modulo>();
        var spl = texto.ToLower().Trim().Split(' ');

        foreach (var s in spl)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                var eEspecificacion1 = new SpecificationCriteriaDirect<Modulo>(
                    m => m.DescModulo.Contains(s));

                especificacion &= eEspecificacion1;
            }
        }

        return especificacion;
    }
}
