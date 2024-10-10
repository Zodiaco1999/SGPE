using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.MenuService;

namespace SGPE.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MenuController : ResponseController
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet("[action]/{idModulo}")]
    public async Task<ActionResult<ServiceResponse>> GetMenusByIdModulo(Guid idModulo)
    {
        SetDataResponse(await _menuService.GetMenusByIdModulo(idModulo));

        return Ok(response);
    }

    [HttpGet("[action]/{idMenu}")]
    public async Task<ActionResult<ServiceResponse>> GetMenu(Guid idMenu)
    {
        SetDataResponse(await _menuService.GetMenu(idMenu));

        return Ok(response);
    }


    [HttpGet("[action]")]
    public async Task<ActionResult<ServiceResponse>> GetMenuUsuario()
    {
        SetDataResponse(await _menuService.GetMenuUsuario());

        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<ServiceResponse>> CreateMenu(MenuDto menuDto)
    {
        SetMessageResponse(await _menuService.CreateMenu(menuDto));

        return Ok(response);
    }

    [HttpPut("[action]")]
    public async Task<ActionResult<ServiceResponse>> UpdateMenu(MenuDto menuDto)
    {
        SetMessageResponse(await _menuService.UpdateMenu(menuDto));

        return Ok(response);
    }

    [HttpGet("[action]/{idMenu}")]
    public async Task<ActionResult<ServiceResponse>> ChangeStatusMenu(Guid idMenu)
    {
        SetMessageResponse(await _menuService.ChangeStatusMenu(idMenu));

        return Ok(response);
    }

}
