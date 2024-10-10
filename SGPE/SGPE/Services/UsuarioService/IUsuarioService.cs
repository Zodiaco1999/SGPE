using SGPE.Comun.Models;

namespace SGPE.WebApi.Services.UsuarioService
{
    public interface IUsuarioService
    {
        Task<PagedResult<UsuarioDto>> GetUsuarios(GetEntityQuery query);
        Task<UsuarioEditDto> GetUsuario(Guid idUsuario);
        Task<string> CreateUsuario(UsuarioEditDto usuario);
        Task<string> UpdateUsuario(UsuarioEditDto usuario);
        Task<string> ChangeStatusUsuario(Guid id);
        Task<Usuario> GetUsuarioById(Guid idUsuario);
        Task<Usuario> GetUsuarioByCedula(string cedula);
        Task<Usuario> GetUserByEmail(string email);
        Task<bool> EmailExists(string email);
    }
}
