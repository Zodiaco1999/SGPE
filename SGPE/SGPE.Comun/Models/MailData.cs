namespace SGPE.Comun.Models;

public class MailData
{
    public string MailBox { get; set; } = string.Empty;
    public string TimeExpiration { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string Tenant { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
}
