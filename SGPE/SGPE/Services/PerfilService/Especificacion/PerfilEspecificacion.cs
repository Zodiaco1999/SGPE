using SGPE.Comun.EspecificacionBase;

namespace SGPE.WebApi.Services.PerfilService.Especificacion;

public class PerfilEspecificacion : SpecificationBase<Perfil>
{
    public PerfilEspecificacion(string texto)
    {
        Criteria = BusquedaTextoCompleto(texto).SatisfiedBy();
    }

    private ISpecificationCriteria<Perfil> BusquedaTextoCompleto(string texto)
    {
        SpecificationCriteria<Perfil> especificacion = new SpecificationCriteriaTrue<Perfil>();
        var spl = texto.ToLower().Trim().Split(' ');

        foreach (var s in spl)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                var eEspecificacion1 = new SpecificationCriteriaDirect<Perfil>(
                    p => p.NombrePerfil.Contains(s)
                    || p.DescPerfil.Contains(s));

                especificacion &= eEspecificacion1;
            }
        }

        return especificacion;
    }
}
