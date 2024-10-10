namespace SGPE.WebApi.Services.EstadoPedidoService
{
    public interface IEstadoPedidoService
    {
        Task<EstadoPedido> GetEstadoPedidoById(long idEstadoPedido);
    }
}