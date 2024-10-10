namespace SGPE.WebApi.Services.EstadoProductoService
{
    public class EstadoProductoService : IEstadoProductoService
    {
        private readonly SGPEContext _db;

        public EstadoProductoService(SGPEContext db)
        {
            _db = db;
        }

        public async Task<EstadoProducto> GetEstadoProductoById(long idEstadoProducto)
        {
            var estadoproducto = await _db.EstadoProductos.FindAsync(idEstadoProducto);
            if (estadoproducto == null)
                throw new Exception("El estado producto no existe");

            return estadoproducto;
        }
    }
}
