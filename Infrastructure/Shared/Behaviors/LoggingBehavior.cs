using System.Text.Json;
using MediatR;
using Serilog;

namespace Shared.Behaviors;

public class LoggingBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken)
    {
        Log.Information("{RequestName} | {Body}", request.GetType().Name, JsonSerializer.Serialize(request));
        return await next();
    }
}