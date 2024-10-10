using SGPE.Comun.Models;

namespace SGPE.WebApi.Services
{
    public class CorreoService : ICorreoService
    {
        private readonly IConfiguration _configuration;
        public CorreoService(IConfiguration configuration) => _configuration = configuration;

        public async Task SendEmail(string asunto, string body, string[] destinatarios, string[] copiados)
        {
            var mailData = new MailData();
            _configuration.Bind("MailData", mailData);

            await new Correo().SendEmail(mailData, asunto, body, destinatarios, copiados);
        }
    }
}
