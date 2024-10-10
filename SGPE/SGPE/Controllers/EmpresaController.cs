using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.EmpresaService;

namespace SGPE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpresaController : ResponseController
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetEmpresas([FromQuery] GetEntityQuery query)
        {
            SetDataResponse(await _empresaService.GetEmpresas(query));

            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ServiceResponse>> GetEmpresa(long id)
        {
            SetDataResponse(await _empresaService.GetEmpresa(id));

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetAllEmpresas()
        {
            SetDataResponse(await _empresaService.GetAllEmpresas());

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetEmpresasWithCategories()
        {
            SetDataResponse(await _empresaService.GetEmpresasWithCategories());

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ServiceResponse>> CreateEmpresa(EmpresaDto empresaDto)
        {
            SetMessageResponse(await _empresaService.CreateEmpresa(empresaDto));

            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ServiceResponse>> UpdateEmpresa(EmpresaDto empresaDto)
        {
            SetMessageResponse(await _empresaService.UpdateEmpresa(empresaDto));

            return Ok(response);
        }
    }
}
