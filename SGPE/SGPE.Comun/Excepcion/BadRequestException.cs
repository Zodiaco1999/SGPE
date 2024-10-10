using System.Net;

namespace SGPE.Comun.Excepcion;

public class BadRequestException : BaseException
{
    public BadRequestException(string message, Exception innerException) : base(message, innerException, (int)HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message, string description) : base(message, description, (int)HttpStatusCode.BadRequest)
    {
    }
}
