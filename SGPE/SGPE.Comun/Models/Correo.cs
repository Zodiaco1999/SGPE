using Independentsoft.Graph.Users;
using Independentsoft.Graph;
using Independentsoft.Graph.Mails;

namespace SGPE.Comun.Models;

public class Correo
{
    public async Task SendEmail(MailData mailData, string asunto, string body, string[] destinatarios, string[] copiados)
    {
        try
        {
            var graphClient = new GraphClient
            {
                ClientId = mailData.ClientId,
                Tenant = mailData.Tenant,
                ClientSecret = mailData.ClientSecret
            };

            Message message = new();
            message.Subject = asunto;
            message.Body = new ItemBody(body, ContentType.Html);

            foreach (string unpara in destinatarios)
                if (!string.IsNullOrEmpty(unpara))
                    message.ToRecipients.Add(new EmailAddress(unpara.Trim()));

            foreach (string uncopiado in copiados)
                if (!string.IsNullOrEmpty(uncopiado))
                    message.CcRecipients.Add(new EmailAddress(uncopiado.Trim()));

            await graphClient.SendMessage(message, new UserId(mailData.MailBox));
        }
        catch (GraphException ex)
        {
            throw new Exception($"Error: {ex.Code}\nMessage: {ex.Message}");
        }
    }
}
