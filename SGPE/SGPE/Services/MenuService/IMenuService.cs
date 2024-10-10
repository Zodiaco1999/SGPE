namespace SGPE.WebApi.Services.MenuService;

public interface IMenuService
{
    Task<IEnumerable<MenuDto>> GetMenusByIdModulo(Guid idModulo);
    Task<IEnumerable<ModuloUsuarioDto>> GetMenuUsuario();
    Task<MenuDto> GetMenu(Guid idMenu);
    Task<string> CreateMenu(MenuDto menuDto);
    Task<string> UpdateMenu(MenuDto menuDto);
    Task<string> ChangeStatusMenu(Guid idMenu);
}