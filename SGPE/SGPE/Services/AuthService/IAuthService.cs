namespace SGPE.WebApi.Services.AuthService;

public interface IAuthService
{
    Task<LoginResult> Login(UsuarioLoginDto dtoAuth);
    Task<string> SendEmailChangeEmail(string email);
    Task<string> ChangeEmail(string email, string token);
    Task<string> SendEmailResetPassword(string cedula);
    Task<string> ChangePassword(ChangePasswordDto userPassword);
    Task<string> ResetPassword(ResetPasswordDto userPassword, string token);
    Task<Usuario> VerifyTokenAndGetUser(string email, string token);
}