namespace SGPE.WebApi.Services.DetallePedidoService
{
    public interface IDetallePedidoService
    {
        Task<IEnumerable<DetallePedidoDto>> GetDetallePedido(Guid idPedido);
    }
}