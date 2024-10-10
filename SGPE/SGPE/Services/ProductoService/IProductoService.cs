using SGPE.Comun.Models;

namespace SGPE.WebApi.Services.ProductoService
{
    public interface IProductoService
    {
        Task<PagedResult<ProductoDto>> GetProductos(GetEntityQuery query);
        Task<ProductoEditDto> GetProducto(long idProducto);
        Task<string> CreateProducto(ProductoEditDto producto);
        Task<string> UpdateProducto(ProductoEditDto producto);
        Task<string> ChangeStatusProducto(long idProducto, long idEstadoProducto);

    }
}