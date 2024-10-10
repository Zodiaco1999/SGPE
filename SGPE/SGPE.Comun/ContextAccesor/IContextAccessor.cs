namespace SGPE.Comun.ContextAccesor;

public interface IContextAccessor
{
    string UserId { get; }
    string UserName { get; } 
    string UserMail { get; }
    string ClientIP { get; } 
    Guid AppId { get; }
    string Headers { get; } 
    string SessionId { get; }
}
