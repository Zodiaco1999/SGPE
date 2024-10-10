using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.ProductoService;

namespace SGPE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ResponseController
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetProductos([FromQuery] GetEntityQuery query)
        {
            SetDataResponse(await _productoService.GetProductos(query));

            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ServiceResponse>> GetProducto(long id)
        {
            SetDataResponse(await _productoService.GetProducto(id));

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ServiceResponse>> CreateProducto(ProductoEditDto producto)
        {
            SetMessageResponse(await _productoService.CreateProducto(producto));

            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ServiceResponse>> UpdateProducto(ProductoEditDto producto)
        {
            SetMessageResponse(await _productoService.UpdateProducto(producto));

            return Ok(response);
        }

        [HttpGet("[action]/{idProducto}/{idEstadoProducto}")]
        public async Task<ActionResult<ServiceResponse>> ChangeStatusProducto(long idProducto, long idEstadoProducto)
        {
            SetMessageResponse(await _productoService.ChangeStatusProducto(idProducto, idEstadoProducto));

            return Ok(response);
        }
    }
}
