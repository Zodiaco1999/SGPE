namespace SGPE.WebApi.Services.EstadoProductoService
{
    public interface IEstadoProductoService
    {
        Task<EstadoProducto> GetEstadoProductoById(long idEstadoProducto);
    }
}