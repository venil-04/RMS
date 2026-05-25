namespace RMS.Application.Exceptions;

public class AppException : Exception
{
    public AppException(string message, ErrorType errorType = ErrorType.BadRequest)
        : base(message)
    {
        ErrorType = errorType;
    }

    public ErrorType ErrorType { get; }
}