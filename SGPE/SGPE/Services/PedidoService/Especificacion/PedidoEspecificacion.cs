using SGPE.Comun.EspecificacionBase;

namespace SGPE.WebApi.Services.PedidoService.Especificacion;

public class PedidoEspecificacion : SpecificationBase<Pedido>
{
    public PedidoEspecificacion(string textoBusqueda)
    {
        Criteria = BusquedaTextoCompleto(textoBusqueda).SatisfiedBy();
    }

    private ISpecificationCriteria<Pedido> BusquedaTextoCompleto(string texto)
    {
        SpecificationCriteria<Pedido> especificacion = new SpecificationCriteriaTrue<Pedido>();
        var spl = texto.ToLower().Trim().Split(' ');

        foreach (var s in spl)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                var eEspecificacion1 = new SpecificationCriteriaDirect<Pedido>(
                    p => p.IdUsuarioSolicitaNavigation.CedulaUsuario.Contains(s)
                    || p.IdUsuarioSolicitaNavigation.Nombres.ToLower().Contains(s)
                    || p.IdUsuarioSolicitaNavigation.Apellidos.ToLower().Contains(s)
                    || p.IdEstadoPedidoNavigation.DescripcionEstadoPedido!.ToLower().Contains(s)
                    || p.ValorTotal.ToString().StartsWith(s));

                especificacion &= eEspecificacion1;
            }
        }

        return especificacion;
    }
}
