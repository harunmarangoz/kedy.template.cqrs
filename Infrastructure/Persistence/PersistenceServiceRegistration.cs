using System.Text.Json;
using Domain.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Implementations;
using Persistence.Interceptors;
using Persistence.Interfaces;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbSettings = new DbConnectionSettings();
        var dbSettingsSection = configuration.GetSection(nameof(DbConnectionSettings));
        dbSettingsSection.Bind(dbSettings);
        services.Configure<DbConnectionSettings>(settings => dbSettingsSection.Bind(settings));

        Console.WriteLine("DbSettings: " + JsonSerializer.Serialize(dbSettings));

        var dbProvider = DbProviderFactory.GetDbProvider(dbSettings);
        services.AddSingleton(dbProvider);
        dbProvider.EnableDatabase(services, dbSettings);
        
        var databaseTypeInstance = services.BuildServiceProvider().GetRequiredService<IDbProvider>();
        
        services.AddTransient<AuditableEntityInterceptor>();
        services.AddTransient<SoftDeleteInterceptor>();
        
        services.AddDbContext<DatabaseContext>((sp, options) =>
        {
            databaseTypeInstance.GetContextBuilder(options, dbSettings);
            options.AddInterceptors(
                sp.GetRequiredService<AuditableEntityInterceptor>(),
                sp.GetRequiredService<SoftDeleteInterceptor>()
            );
        });
    }

    public static void ConfigurePersistenceInfrastructure(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
        context?.Database.Migrate();
    }

    private static void RegisterDatabaseType(IServiceCollection services,
        DbConnectionSettings dbConnectionSettings)
    {
        var databaseInterfaceType = typeof(IDbProvider);
        var instanceType = dbConnectionSettings.ProviderType;
        var instance = databaseInterfaceType.Assembly.GetTypes().FirstOrDefault(x =>
            databaseInterfaceType.IsAssignableFrom(x) &&
            string.Equals(instanceType, x.Name, StringComparison.OrdinalIgnoreCase));
        services.AddSingleton((IDbProvider)Activator.CreateInstance(instance));
    }
}

public class DbProviderFactory
{
    public static IDbProvider GetDbProvider(DbConnectionSettings dbConnectionSettings)
    {
        var databaseInterfaceType = typeof(IDbProvider);
        var instanceType = dbConnectionSettings.ProviderType;
        var instance = databaseInterfaceType.Assembly.GetTypes().FirstOrDefault(x =>
            databaseInterfaceType.IsAssignableFrom(x) &&
            string.Equals(instanceType, x.Name, StringComparison.OrdinalIgnoreCase));
        return (IDbProvider)Activator.CreateInstance(instance);
    }
}