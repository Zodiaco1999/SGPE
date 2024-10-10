using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SGPE.Comun.ContextAccesor;
using SGPE.Comun.Excepcion;
using SGPE.Comun.Extensions;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.ModuloService.Especificacion;
using System.Data;

namespace SGPE.WebApi.Services.ModuloService;

public class ModuloService : IModuloService
{
    private readonly SGPEContext _db;
    private readonly IMapper _mapper;
    private readonly IContextAccessor _contextAccessor;

    public ModuloService(SGPEContext db, IMapper mapper, IContextAccessor contextAccessor)
    {
        _db = db;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<PagedResult<ModuloDto>> GetModulos(GetEntityQuery query)
    {
        var especificacion = new ModuloEspecificacion(query.SearchText ?? "");
        query.SortProperty = string.IsNullOrEmpty(query.SortProperty) ? "orden" : query.SortProperty;

        return await _db.Modulos
            .AsNoTracking()
            .Where(especificacion.Criteria)
            .OrderBy($"{query.SortProperty} {query.SortDir}")
            .ProjectTo<ModuloDto>(_mapper.ConfigurationProvider)
            .GetPagedResultAsync(query.PageSize, query.CurrentPage);
    }

    public async Task<ModuloDto> GetModulo(Guid idModulo)
    {
        var modulo = await GetModuloById(idModulo);
        var moduloDto = _mapper.Map<ModuloDto>(modulo);

        return moduloDto;
    }

    public async Task<string> CreateModulo(ModuloDto moduloDto)
    {
        var newModulo = _mapper.Map<Modulo>(moduloDto);
        newModulo.IdModulo = Guid.NewGuid();
        newModulo.CreaMaquina = _contextAccessor.ClientIP;
        newModulo.CreaUsuario = _contextAccessor.UserName ?? "N/A";
        newModulo.CreaFecha = DateTime.Now;

        newModulo.Menus.ToList().ForEach(menu =>
        {
            menu.IdMenu = Guid.NewGuid();
            menu.CreaMaquina = _contextAccessor.ClientIP;
            menu.CreaUsuario = _contextAccessor.UserName ?? "N/A";
        });

        _db.Modulos.Add(newModulo);
        await _db.SaveChangesAsync();

        return $"¡Modulo {moduloDto.NombreModulo} creado!";
    }

    public async Task<string> UpdateModulo(ModuloDto moduloDto)
    {
        var modulo = await _db.Modulos
            .Include(m => m.Menus)
            .FirstOrDefaultAsync(m => m.IdModulo == moduloDto.IdModulo) ?? throw new NotFoundException(nameof(Modulo), moduloDto.IdModulo);

        modulo.NombreModulo = moduloDto.NombreModulo;
        modulo.DescModulo = moduloDto.DescModulo;
        modulo.IconoPrefijo = moduloDto.IconoPrefijo;
        modulo.IconoNombre = moduloDto.IconoNombre;
        modulo.Orden = moduloDto.Orden;
        modulo.ModificaMaquina = _contextAccessor.ClientIP;
        modulo.ModificaUsuario = _contextAccessor.UserName ?? "N/A";
        modulo.ModificaFecha = DateTime.Now;

        var menusRegistrados = modulo.Menus.ToList();
        modulo.Menus.Clear();

        var menusEntrantes = _mapper.Map<List<Menu>>(moduloDto.Menus);

        foreach (var menu in menusEntrantes)
        {
            var menuExistente = menusRegistrados.FirstOrDefault(m => m.IdMenu == menu.IdMenu);

            if (menuExistente != null)
            {
                menu.CreaMaquina = menuExistente.CreaMaquina;
                menu.CreaUsuario = menuExistente.CreaUsuario;
                menu.CreaFecha = menuExistente.CreaFecha;
                menu.ModificaMaquina = _contextAccessor.ClientIP;
                menu.ModificaUsuario = _contextAccessor.UserName ?? "N/A";
                menu.ModificaFecha = DateTime.Now;
            }
            else
            {
                menu.CreaMaquina = _contextAccessor.ClientIP;
                menu.CreaUsuario = _contextAccessor.UserName ?? "N/A";
            }

            modulo.Menus.Add(menu);
        }

        await _db.SaveChangesAsync();

        return $"¡Modulo {moduloDto.NombreModulo} actualizado!";
    }

    public async Task<string> ChangeStatusModulo(Guid idModulo)
    {
        var modulo = await GetModuloById(idModulo);
        modulo.Activo = !modulo.Activo;

        await _db.SaveChangesAsync();

        return $"Se {(modulo.Activo ? "activo" : "inactivo")} el modulo {modulo.NombreModulo} correctamente";
    }

    private async Task<Modulo> GetModuloById(Guid idModulo)
    {
        return await _db.Modulos.FindAsync(idModulo)
            ?? throw new NotFoundException(nameof(Modulo), idModulo);
    }
}
