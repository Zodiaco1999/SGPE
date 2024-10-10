using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.PedidoService;

namespace SGPE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PedidoController : ResponseController
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetPedidos([FromQuery] GetEntityQuery query)
        {
            SetDataResponse(await _pedidoService.GetPedidos(query));

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ServiceResponse>> GetPedidosUsuario([FromQuery] GetEntityQuery query)
        {
            SetDataResponse(await _pedidoService.GetPedidosUsuario(query));

            return Ok(response);
        }

        [HttpGet("[action]/{idEmpresa}")]
        public async Task<ActionResult<ServiceResponse>> GetProductosPedido(long idEmpresa)
        {
            SetDataResponse(await _pedidoService.GetProductosPedido(idEmpresa));

            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ServiceResponse>> GetPedido(Guid id)
        {
            SetDataResponse(await _pedidoService.GetPedido(id));

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ServiceResponse>> CreatePedido(List<DetallePedidoEditDto> detalles)
        {
            SetMessageResponse(await _pedidoService.CreatePedido(detalles));

            return response;
        }

        [HttpGet("[action]/{idPedido}/{idEstadoPedido}")]

        public async Task<ActionResult<ServiceResponse>> ChangeStatusPedido(Guid idPedido, long idEstadoPedido)
        {
            SetDataResponse(await _pedidoService.ChangeStatusPedido(idPedido, idEstadoPedido));

            return Ok(response);
        }
    }
}
