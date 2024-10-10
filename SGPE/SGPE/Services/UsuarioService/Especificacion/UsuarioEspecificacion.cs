using SGPE.Comun.EspecificacionBase;

namespace SGPE.WebApi.Services.UsuarioService.Especificacion;

public class UsuarioEspecificacion : SpecificationBase<Usuario>
{
    public UsuarioEspecificacion(string textoBusqueda)
    {
        Criteria = BusquedaTextoCompleto(textoBusqueda).SatisfiedBy();
    }

    private ISpecificationCriteria<Usuario> BusquedaTextoCompleto(string texto)
    {
        SpecificationCriteria<Usuario> especificacion = new SpecificationCriteriaTrue<Usuario>();
        var spl = texto.ToLower().Trim().Split(' ');

        foreach (var s in spl)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                var eEspecificacion1 = new SpecificationCriteriaDirect<Usuario>(
                    u => u.CedulaUsuario.StartsWith(s)
                    || u.Nombres.ToLower().Contains(s.ToLower())
                    || u.Apellidos.ToLower().Contains(s.ToLower())
                    || u.Correo.StartsWith(s));

                especificacion &= eEspecificacion1;
            }
        }

        return especificacion;
    }
}
