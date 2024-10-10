using SGPE.Comun.Models;

namespace SGPE.WebApi.Services.ModuloService;

public interface IModuloService
{
    Task<PagedResult<ModuloDto>> GetModulos(GetEntityQuery query);
    Task<ModuloDto> GetModulo(Guid idModulo);
    Task<string> CreateModulo(ModuloDto moduloDto);
    Task<string> UpdateModulo(ModuloDto moduloDto);
    Task<string> ChangeStatusModulo(Guid idModulo);
}