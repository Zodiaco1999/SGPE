namespace SGPE.WebApi.Services;

public interface ICorreoService
{
    Task SendEmail(string asunto, string body, string[] destinatarios, string[] copiados);
}