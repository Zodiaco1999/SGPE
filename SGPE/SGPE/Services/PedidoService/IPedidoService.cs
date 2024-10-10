using SGPE.Comun.Models;

namespace SGPE.WebApi.Services.PedidoService
{
    public interface IPedidoService
    {
        Task<PagedResult<PedidoDto>> GetPedidos(GetEntityQuery query);
        Task<PagedResult<PedidoDto>> GetPedidosUsuario(GetEntityQuery query);
        Task<IEnumerable<ProductoPedidoDto>> GetProductosPedido(long idEmpresa);
        Task<PedidoDto> GetPedido(Guid idPedido);
        Task<string> CreatePedido(List<DetallePedidoEditDto> detalles);
        Task<PedidoEditDto> UpdatePedido(PedidoEditDto pedido);
        Task<string> ChangeStatusPedido(Guid idPedido, long idEstadoPedido);

    }
}