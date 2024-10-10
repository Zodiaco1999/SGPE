using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SGPE.Comun.ContextAccesor;
using SGPE.Comun.Excepcion;
using SGPE.Comun.Extensions;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.PerfilService.Especificacion;

namespace SGPE.WebApi.Services.PerfilService;

public class PerfilService : IPerfilService
{
    private readonly SGPEContext _db;
    private readonly IMapper _mapper;
    private readonly IContextAccessor _contextAccessor;

    public PerfilService(SGPEContext db, IMapper mapper, IContextAccessor contextAccessor)
    {
        _db = db;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<PagedResult<PerfilDto>> GetPerfiles(GetEntityQuery query)
    {
        var especificacion = new PerfilEspecificacion(query.SearchText ?? "");

        return await _db.Perfils
                .AsNoTracking()
                .Where(especificacion.Criteria)
                .OrderBy($"{query.SortProperty} {query.SortDir}")
                .Select(p => new PerfilDto
                {
                    IdPerfil = p.IdPerfil,
                    NombrePerfil = p.NombrePerfil,
                    DescPerfil = p.DescPerfil,
                    Activo = p.Activo
                })
                .GetPagedResultAsync(query.PageSize, query.CurrentPage);
    }

    public async Task<PerfilDto> GetPerfil(Guid idPerfil)
    {
        return await _db.Perfils
            .Where(p => p.IdPerfil == idPerfil)
            .ProjectTo<PerfilDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException(nameof(Perfil), idPerfil);
    }

    public async Task<IEnumerable<PerfilDto>> GetActivePerfiles()
    {
        return await _db.Perfils
            .Where(p => p.Activo)
            .Select(p => new PerfilDto
            {
                IdPerfil = p.IdPerfil,
                NombrePerfil = p.NombrePerfil
            }).ToListAsync();
    }

    public async Task<string> CreatePerfil(PerfilEditDto perfilDto)
    {
        var newPerfil = _mapper.Map<Perfil>(perfilDto);
        newPerfil.IdPerfil = Guid.NewGuid();
        newPerfil.CreaMaquina = _contextAccessor.ClientIP;
        newPerfil.CreaUsuario = _contextAccessor.UserName ?? "N/A";
        newPerfil.CreaFecha = DateTime.Now;

        _db.Perfils.Add(newPerfil);
        await _db.SaveChangesAsync();

        return "¡Pefil creado!";
    }

    public async Task<string> UpdatePerfil(PerfilEditDto perfilDto)
    {
        var updatePerfil = await GetPerfilById(perfilDto.IdPerfil);

        var perfilMenus = _mapper.Map<List<PerfilMenu>>(perfilDto.PerfilMenus);
        await _db.PerfilMenus.Where(pm => pm.IdPerfil == perfilDto.IdPerfil).ExecuteDeleteAsync();

        updatePerfil.NombrePerfil = perfilDto.NombrePerfil;
        updatePerfil.DescPerfil = perfilDto.DescPerfil;
        updatePerfil.ModificaMaquina = _contextAccessor.ClientIP;
        updatePerfil.ModificaUsuario = _contextAccessor.UserName ?? "N/A";
        updatePerfil.ModificaFecha = DateTime.Now;
        updatePerfil.PerfilMenus = perfilMenus;

        await _db.SaveChangesAsync();

        return "¡Perfil actualizado!";
    }

    public async Task<string> ChangeStatusPerfil(Guid idPerfil)
    {
        var perfil = await GetPerfilById(idPerfil);
        perfil.Activo = !perfil.Activo;

        await _db.SaveChangesAsync();

        return $"Se {(perfil.Activo ? "activo" : "inactivo")} el perfil {perfil.NombrePerfil} correctamente";
    }

    public async Task<IEnumerable<PerfilMenuDto>> GetActiveMenus()
    {
        return await _db.Menus
            .Where(m => m.IdModuloNavigation.Activo && m.Activo)
            .OrderBy(m => m.IdModuloNavigation.NombreModulo).ThenBy(m => m.NombreMenu)
            .ProjectTo<PerfilMenuDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    private async Task<Perfil> GetPerfilById(Guid idPerfil)
    {
        return await _db.Perfils.FindAsync(idPerfil)
            ?? throw new NotFoundException(nameof(Perfil), idPerfil);
    }
}
