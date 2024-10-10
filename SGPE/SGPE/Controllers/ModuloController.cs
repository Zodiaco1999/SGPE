using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.ModuloService;

namespace SGPE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModuloController : ResponseController
    {
        private readonly IModuloService _moduloService;

        public ModuloController(IModuloService moduloService)
        {
            _moduloService = moduloService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetModulos([FromQuery] GetEntityQuery query)
        {
            SetDataResponse(await _moduloService.GetModulos(query));

            return Ok(response);
        }

        [HttpGet("[action]/{idModulo}")]
        public async Task<ActionResult<ServiceResponse>> GetModulo(Guid idModulo)
        {
            SetDataResponse(await _moduloService.GetModulo(idModulo));

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ServiceResponse>> CreateModulo(ModuloDto moduloDto)
        {
            SetMessageResponse(await _moduloService.CreateModulo(moduloDto));

            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ServiceResponse>> UpdateModulo(ModuloDto moduloDto)
        {
            SetMessageResponse(await _moduloService.UpdateModulo(moduloDto));

            return Ok(response);
        }

        [HttpGet("[action]/{idModulo}")]
        public async Task<ActionResult<ServiceResponse>> ChangeStatusModulo(Guid idModulo)
        {
            SetMessageResponse(await _moduloService.ChangeStatusModulo(idModulo));

            return Ok(response);
        }
    }
}
