using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Middlewares;

namespace Api.Configurations;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });
        services.AddRouting(options => options.LowercaseUrls = true);

        services
            .AddCors(options => options.AddPolicy("AllowAll", p =>
                p.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:4200", "https://localhost:7023", "http://localhost:5124")));

        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static void ConfigureApiConfiguration(this WebApplication app)
    {
        app.MapControllers();
        app.UseCors("AllowAll");
        app.UseExceptionHandler();
    }
}

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString() ?? string.Empty);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
    }
}