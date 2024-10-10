namespace SGPE.Comun.Excepcion;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}
