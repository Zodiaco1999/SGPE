using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X500;
using SGPE.Comun.ContextAccesor;
using SGPE.Comun.Excepcion;
using SGPE.WebApi.DataAccess;
using System;

namespace SGPE.WebApi.Services.MenuService;

public class MenuService : IMenuService
{
    private readonly SGPEContext _db;
    private readonly IMapper _mapper;
    private readonly IContextAccessor _contextAccessor;

    public MenuService(SGPEContext db, IMapper mapper, IContextAccessor contextAccessor)
    {
        _db = db;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<IEnumerable<MenuDto>> GetMenusByIdModulo(Guid idModulo)
    {
        return await _db.Menus
            .ProjectTo<MenuDto>(_mapper.ConfigurationProvider)
            .Where(m => m.IdModulo == idModulo)
            .ToListAsync();
    }

    public async Task<MenuDto> GetMenu(Guid idMenu)
    {
        var menu = await GetMenuById(idMenu);

        return _mapper.Map<MenuDto>(menu);
    }

    public async Task<IEnumerable<ModuloUsuarioDto>> GetMenuUsuario()
    {
        var perfilMenus = await _db.PerfilMenus
            .Where(p => p.IdPerfilNavigation.Activo && p.IdModuloNavigation.Activo && p.IdM.Activo &&
                   p.IdPerfilNavigation.UsuarioPerfils.FirstOrDefault(u => u.IdUsuario == Guid.Parse(_contextAccessor.UserId)) != null)
            .Include(p => p.IdM.IdModuloNavigation)
            .ToListAsync();

        var modulosUsuario = perfilMenus
            .GroupBy(p => p.IdM.IdModuloNavigation)
            .OrderBy(p => p.Key.Orden)
            .Select(gModulo => new ModuloUsuarioDto
            {
                IdModulo = gModulo.Key.IdModulo,
                NombreModulo = gModulo.Key.NombreModulo,
                DescModulo = gModulo.Key.DescModulo,
                IconoPrefijo = gModulo.Key.IconoPrefijo,
                IconoNombre = gModulo.Key.IconoNombre,
                MenusUsuario = gModulo.GroupBy(p => p.IdM)
                .OrderBy(p => p.Key.Orden)
                .Select(gMenu => new MenuUsuarioDto
                {
                    IdMenu = gMenu.Key.IdMenu,
                    NombreMenu = gMenu.Key.NombreMenu,
                    EtiquetaMenu = gMenu.Key.EtiquetaMenu,
                    Url = gMenu.Key.Url,
                    Orden = gMenu.Key.Orden,
                    Consulta = gMenu.Key.Consulta,
                    Inserta = gMenu.Key.Inserta,
                    Actualiza = gMenu.Key.Actualiza,
                    Elimina = gMenu.Key.Elimina,
                    Activa = gMenu.Key.Activa,
                    Ejecuta = gMenu.Key.Ejecuta,
                    MenuConsulta = gMenu.Max(m => m.Consulta) && gMenu.Key.Consulta,
                    MenuInserta = gMenu.Max(m => m.Inserta) && gMenu.Key.Inserta,
                    MenuActualiza = gMenu.Max(m => m.Actualiza) && gMenu.Key.Actualiza,
                    MenuElimina = gMenu.Max(m => m.Elimina) && gMenu.Key.Elimina,
                    MenuActiva = gMenu.Max(m => m.Activa) && gMenu.Key.Activa,
                    MenuEjecuta = gMenu.Max(m => m.Ejecuta) && gMenu.Key.Ejecuta
                })
            });

        return modulosUsuario;
    }

    public async Task<string> CreateMenu(MenuDto menuDto)
    {
        var newMenu = _mapper.Map<Menu>(menuDto);

        newMenu.IdMenu = Guid.NewGuid();
        newMenu.CreaMaquina = _contextAccessor.ClientIP;
        newMenu.CreaUsuario = _contextAccessor.UserName ?? "N/A";
        newMenu.CreaFecha = DateTime.Now;
        newMenu.Activo = true;

        _db.Menus.Add(newMenu);
        await _db.SaveChangesAsync();

        return "¡Menú creado!";
    }

    public async Task<string> UpdateMenu(MenuDto menuDto)
    {
        var menu = await GetMenuById(menuDto.IdMenu);

        menu.NombreMenu = menuDto.NombreMenu;
        menu.DescMenu = menuDto.DescMenu;
        menu.EtiquetaMenu = menuDto.EtiquetaMenu;
        menu.Url = menuDto.Url;
        menu.Orden = menuDto.Orden;
        menu.Consulta = menuDto.Consulta;
        menu.Inserta = menuDto.Inserta;
        menu.Actualiza = menuDto.Actualiza;
        menu.Elimina = menuDto.Elimina;
        menu.Activa = menuDto.Activa;
        menu.Ejecuta = menuDto.Ejecuta;
        menu.ModificaMaquina = _contextAccessor.ClientIP;
        menu.ModificaUsuario = _contextAccessor.UserName ?? "N/A";
        menu.ModificaFecha = DateTime.Now;

        await _db.SaveChangesAsync();

        return "¡Menú Actualizado!";
    }

    public async Task<string> ChangeStatusMenu(Guid idMenu)
    {
        var menu = await GetMenuById(idMenu);
        menu.Activo = !menu.Activo;

        await _db.SaveChangesAsync();

        return $"Menú {(menu.Activo ? "activado" : "inactivado")} correctamente";
    }

    private async Task<Menu> GetMenuById(Guid idMenu)
    {
        return await _db.Menus.FirstOrDefaultAsync(m => m.IdMenu == idMenu)
            ?? throw new NotFoundException(nameof(Menu), idMenu);
    }


}
