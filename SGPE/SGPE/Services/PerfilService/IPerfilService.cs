using SGPE.Comun.Models;

namespace SGPE.WebApi.Services.PerfilService;

public interface IPerfilService
{
    Task<PagedResult<PerfilDto>> GetPerfiles(GetEntityQuery query);
    Task<PerfilDto> GetPerfil(Guid idPerfil);
    Task<IEnumerable<PerfilDto>> GetActivePerfiles();
    Task<string> CreatePerfil(PerfilEditDto perfilDto);
    Task<string> UpdatePerfil(PerfilEditDto perfilDto);
    Task<string> ChangeStatusPerfil(Guid idPerfil);
    Task<IEnumerable<PerfilMenuDto>> GetActiveMenus();
}