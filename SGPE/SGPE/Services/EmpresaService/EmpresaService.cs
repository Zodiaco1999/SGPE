using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SGPE.Comun.Excepcion;
using SGPE.Comun.Extensions;
using SGPE.Comun.Models;

namespace SGPE.WebApi.Services.EmpresaService;

public class EmpresaService : IEmpresaService
{
    private readonly SGPEContext _db;
    private readonly IMapper _mapper;

    public EmpresaService(SGPEContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PagedResult<EmpresaDto>> GetEmpresas(GetEntityQuery query)
    {
        return await _db.Empresas
            .OrderBy($"{query.SortProperty} {query.SortDir}")
            .ProjectTo<EmpresaDto>(_mapper.ConfigurationProvider)
            .GetPagedResultAsync(query.PageSize, query.CurrentPage);
    }

    public async Task<IEnumerable<EmpresaDto>> GetAllEmpresas()
    {
        return await _db.Empresas
            .ProjectTo<EmpresaDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<EmpresaDto>> GetEmpresasWithCategories()
    {
        return await _db.Empresas
            .Where(e => e.CategoriaProductos.Any() && e.CategoriaProductos.Where(c => c.Productos.Any()).Any())
            .ProjectTo<EmpresaDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<EmpresaDto> GetEmpresa(long idEmpresa)
    {
        var empresa = await GetEmpresaById(idEmpresa);
        var empresaDto = _mapper.Map<EmpresaDto>(empresa);

        return empresaDto;
    }

    public async Task<Empresa> GetEmpresaById(long idEmpresa)
    {
        return await _db.Empresas.FindAsync(idEmpresa) ??
            throw new NotFoundException(nameof(Empresa), idEmpresa);
    }

    public async Task<string> CreateEmpresa(EmpresaDto empresaDto)
    {
        var newEmpresa = _mapper.Map<Empresa>(empresaDto);

        _db.Empresas.Add(newEmpresa);
        await _db.SaveChangesAsync();

        return $"¡Se creo la empresa {newEmpresa.NombreEmpresa} correctamente!";
    }

    public async Task<string> UpdateEmpresa(EmpresaDto empresaDto)
    {
        var empresa = await GetEmpresaById(empresaDto.IdEmpresa);
        empresa.Nit = empresaDto.Nit;
        empresa.NombreEmpresa = empresaDto.NombreEmpresa;

        await _db.SaveChangesAsync();

        return $"¡Se actualizo la empresa {empresa.NombreEmpresa} correctamente!";
    }
}
