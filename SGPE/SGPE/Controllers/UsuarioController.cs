using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.UsuarioService;

namespace SGPE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ResponseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("[action]")]
        public async Task<ServiceResponse> GetUsuarios([FromQuery] GetEntityQuery query)
        {
            SetDataResponse(await _usuarioService.GetUsuarios(query));

            return response;
        }

        [HttpGet("[action]/{idUsuario}")]
        public async Task<ServiceResponse> GetUsuario(Guid idUsuario)
        {
            SetDataResponse(await _usuarioService.GetUsuario(idUsuario));

            return response;
        }

        [HttpPost("[action]")]
        public async Task<ServiceResponse> CreateUsuario(UsuarioEditDto usuario)
        {
            SetMessageResponse(await _usuarioService.CreateUsuario(usuario));

            return response;
        }

        [HttpPut("[action]")]
        public async Task<ServiceResponse> UpdateUsuario(UsuarioEditDto usuarioDto)
        {
            SetMessageResponse(await _usuarioService.UpdateUsuario(usuarioDto));

            return response;
        }

        [HttpGet("[Action]/{id}")]
        public async Task<ServiceResponse> ChangeStatusUsuario(Guid id)
        {
            SetMessageResponse(await _usuarioService.ChangeStatusUsuario(id));

            return response;
        }
    }
}
