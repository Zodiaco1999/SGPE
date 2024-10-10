using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.PerfilService;

namespace SGPE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PerfilController : ResponseController
    {
        private readonly IPerfilService _perfilService;
        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetPerfiles([FromQuery] GetEntityQuery query)
        {
            SetDataResponse(await _perfilService.GetPerfiles(query));

            return Ok(response);
        }

        [HttpGet("[action]/{idPerfil}")]
        public async Task<ActionResult<ServiceResponse>> GetPerfil(Guid idPerfil)
        {
            SetDataResponse(await _perfilService.GetPerfil(idPerfil));

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetActivePerfiles()
        {
            SetDataResponse(await _perfilService.GetActivePerfiles());

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ServiceResponse>> CreatePerfil(PerfilEditDto perfilDto)
        {
            SetMessageResponse(await _perfilService.CreatePerfil(perfilDto));

            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ServiceResponse>> UpdatePerfil(PerfilEditDto perfilDto)
        {
            SetMessageResponse(await _perfilService.UpdatePerfil(perfilDto));

            return Ok(response);
        }

        [HttpGet("[action]/{idPerfil}")]
        public async Task<ActionResult<ServiceResponse>> ChangeStatusPerfil(Guid idPerfil)
        {
            SetMessageResponse(await _perfilService.ChangeStatusPerfil(idPerfil));

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetActiveMenus()
        {
            SetDataResponse(await _perfilService.GetActiveMenus());

            return Ok(response);
        }
    }
}
