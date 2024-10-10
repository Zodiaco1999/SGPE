using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGPE.Comun.Excepcion;
using SGPE.Comun.Models;
using SGPE.WebApi.Services.AuthService;

namespace SGPE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ResponseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse>> Login(UsuarioLoginDto usarioDto)
        {
            SetDataResponse(await _authService.Login(usarioDto));

            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ServiceResponse>> ChangePassword(ChangePasswordDto changePassword)
        {
            SetMessageResponse(await _authService.ChangePassword(changePassword));

            return Ok(response);
        }

        [HttpGet("[action]/{email}")]
        public async Task<ActionResult<ServiceResponse>> SendEmailChangeEmail(string email)
        {
            SetMessageResponse(await _authService.SendEmailChangeEmail(email));

            return Ok(response);
        }

        [HttpGet("[action]/{email}/{token}")]
        public async Task<ActionResult<ServiceResponse>> ChangeEmail(string email, string token)
        {
            SetMessageResponse(await _authService.ChangeEmail(email, token));

            return Ok(response);
        }

        [HttpGet("[action]/{cedula}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse>> SendEmailResetPassword(string cedula)
        {
            SetMessageResponse(await _authService.SendEmailResetPassword(cedula));

            return Ok(response);
        }

        [HttpPut("[action]/{token}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse>> ResetPassword(ResetPasswordDto user, string token)
        {
            await _authService.ResetPassword(user, token);
            SetMessageResponse("Contraseña cambiada correctamente");

            return Ok(response);
        }

        [HttpGet("[action]/{email}/{token}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse>> VerifyToken(string email, string token)
        {
            await _authService.VerifyTokenAndGetUser(email, token);

            return Ok(response);
        }

    }
}
