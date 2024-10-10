using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.DetallePedidoService;

namespace SGPE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DetallePedidoController : ResponseController
    {
        private readonly IDetallePedidoService _detallePedidoService;

        public DetallePedidoController(IDetallePedidoService detallePedidoService)
        {
            _detallePedidoService = detallePedidoService;
        }

        [HttpGet("[action]/{idPedido}")]
        public async Task<ActionResult<ServiceResponse>> GetDetallePedido(Guid idPedido)
        {
            SetDataResponse(await _detallePedidoService.GetDetallePedido(idPedido));
           
            return Ok(response);
        }
    }
}