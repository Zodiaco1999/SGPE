using SGPE.Comun.Models;

namespace SGPE.WebApi.Services.CategoriaProductoService
{
    public interface ICategoriaProductoService
    {
        Task<PagedResult<CategoriaProductoDto>> GetCategorias(GetEntityQuery query);
        Task<IEnumerable<CategoriaProductoDto>> GetAllCategorias();
        Task<CategoriaProductoDto> GetCategoria(long idCategoria);
        Task<IEnumerable<CategoriaProductoDto>> GetCategoriasByIdEmpresa(long idEmpresa);
        Task<string> CreateCategoria(CategoriaProductoDto categoriaProducto);
        Task<string> UpdateCategoria(CategoriaProductoDto categoriaProducto);
        Task<string> ChangeStatusCategoria(long idCategoria);
    }
}
