using Domain.Exceptions;
using Kedy.Result;
using Kedy.Result.Response;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace Api.Middlewares;
public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        Log.Error(exception.Message);

        var message = GetMessage(exception);
        var result = new ErrorResult(message);

        if (exception is AppValidationException validationException)
            result.Errors = validationException.Errors;

        var response = new Response(result);
        httpContext.Response.StatusCode = GetStatusCode(exception);
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }

    private static string GetMessage(Exception exception)
    {
        if (exception is IDisplayException displayException)
            return displayException.Message;
        return "Internal Server Error";
    }

    private static int GetStatusCode(Exception exception)
    {
        if (exception is not AppException) return StatusCodes.Status500InternalServerError;
        if (exception is AppValidationException)
            return StatusCodes.Status422UnprocessableEntity;
        return StatusCodes.Status400BadRequest;
    }
}