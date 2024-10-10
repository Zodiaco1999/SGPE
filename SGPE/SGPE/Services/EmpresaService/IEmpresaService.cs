using SGPE.Comun.Models;

namespace SGPE.WebApi.Services.EmpresaService;

public interface IEmpresaService
{
    Task<PagedResult<EmpresaDto>> GetEmpresas(GetEntityQuery query);
    Task<EmpresaDto> GetEmpresa(long idEmpresa);
    Task<IEnumerable<EmpresaDto>> GetAllEmpresas();
    Task<IEnumerable<EmpresaDto>> GetEmpresasWithCategories();
    Task<string> CreateEmpresa(EmpresaDto empresaDto);
    Task<string> UpdateEmpresa(EmpresaDto empresaDto);
}
