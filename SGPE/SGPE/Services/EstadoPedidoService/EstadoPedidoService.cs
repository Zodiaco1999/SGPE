namespace SGPE.WebApi.Services.EstadoPedidoService
{
    public class EstadoPedidoService : IEstadoPedidoService
    {
        private readonly SGPEContext _db;
        public EstadoPedidoService(SGPEContext db)
        {
            _db = db;
        }
        public async Task<EstadoPedido> GetEstadoPedidoById(long idEstadoPedido)
        {
            var estadoPedido = await _db.EstadoPedidos.FindAsync(idEstadoPedido);
            if (estadoPedido == null)
                throw new Exception("Estado del pedido no existe");

            return estadoPedido;
        }

    }
}
