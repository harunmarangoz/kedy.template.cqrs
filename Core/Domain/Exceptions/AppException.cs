using System.Globalization;

namespace Domain.Exceptions;

public class AppException : Exception
{
    public AppException() : base()
    {
    }

    public AppException(string message) : base(message)
    {
    }

    public AppException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }

    public AppException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class AppValidationException(List<KeyValuePair<string, string>> errors, string message)
    : AppException(message), IDisplayException
{
    public AppValidationException(List<KeyValuePair<string, string>> errors) : this(errors, "Validation Error")
    {
    }

    public List<KeyValuePair<string, string>> Errors { get; set; } = errors;
}