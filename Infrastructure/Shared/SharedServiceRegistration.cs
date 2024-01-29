using Application.Services;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviors;
using Shared.Services;

namespace Shared;

public static class SharedServiceRegistration
{
    public static void AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<IWorkContext, DefaultWorkContext>();
        
        services.AddScoped<IMapper, Mapper>();
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }
}