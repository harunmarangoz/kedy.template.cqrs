using System.Data.Common;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Persistence.Interfaces;

namespace Persistence.Implementations.DbProviders;

public class PostgreSqlDbProvider : IDbProvider
{
    public IServiceCollection EnableDatabase(IServiceCollection services, DbConnectionSettings connectionStringSettings)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        return services;
    }

    public DbConnectionStringBuilder GetConnectionBuilder(string connectionString)
    {
        return new NpgsqlConnectionStringBuilder(connectionString);
    }

    public DbContextOptionsBuilder GetContextBuilder(DbContextOptionsBuilder optionsBuilder, DbConnectionSettings dbConnectionSettings)
    {
        return optionsBuilder.UseNpgsql(dbConnectionSettings.ConnectionString);
    }

    public DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(
        DbContextOptionsBuilder<TContext> contextOptionsBuilder, string connectionString) where TContext : DbContext
    {
        return contextOptionsBuilder.UseNpgsql(connectionString);
    }
}