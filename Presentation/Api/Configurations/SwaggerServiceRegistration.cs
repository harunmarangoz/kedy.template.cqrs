using Microsoft.OpenApi.Models;

namespace Api.Configurations;

public static class SwaggerServiceRegistration
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api",
                Version = "v1"
            });
            c.EnableAnnotations();
        });
    }

    public static void ConfigureSwaggerConfiguration(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}