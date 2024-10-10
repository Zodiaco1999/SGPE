using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.CategoriaProductoService;

namespace SGPE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaProductoController : ResponseController
    {
        private readonly ICategoriaProductoService _cagoriaProducto;

        public CategoriaProductoController(ICategoriaProductoService categoriaProducto)
        {
            _cagoriaProducto = categoriaProducto;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetCategorias([FromQuery] GetEntityQuery query)
        {
            SetDataResponse(await _cagoriaProducto.GetCategorias(query));

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetAllCategorias()
        {
            SetDataResponse(await _cagoriaProducto.GetAllCategorias());

            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ServiceResponse>> GetCategoria(long id)
        {
            SetDataResponse(await _cagoriaProducto.GetCategoria(id));

            return Ok(response);
        }

        [HttpGet("[action]/{idEmpresa}")]
        public async Task<ActionResult<ServiceResponse>> GetCategoriasByIdEmpresa(long idEmpresa)
        {
            SetDataResponse(await _cagoriaProducto.GetCategoriasByIdEmpresa(idEmpresa));

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ServiceResponse>> CreateCategoria(CategoriaProductoDto categoria)
        {
            SetMessageResponse(await _cagoriaProducto.CreateCategoria(categoria));

            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ServiceResponse>> UpdateCategoria(CategoriaProductoDto categoria)
        {
            SetMessageResponse(await _cagoriaProducto.UpdateCategoria(categoria));

            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ServiceResponse>> ChangeStatusCategoria(long id)
        {
            SetMessageResponse(await _cagoriaProducto.ChangeStatusCategoria(id));

            return Ok(response);
        }
    }
}
