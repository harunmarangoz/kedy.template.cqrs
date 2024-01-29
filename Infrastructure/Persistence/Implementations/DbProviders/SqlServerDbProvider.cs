using System.Data.Common;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interfaces;

namespace Persistence.Implementations.DbProviders;

public class SqlServerDbProvider : IDbProvider
{
    public IServiceCollection EnableDatabase(IServiceCollection services, DbConnectionSettings connectionStringSettings)
    {
        return services;
    }

    public DbConnectionStringBuilder GetConnectionBuilder(string connectionString)
    {
        return new DbConnectionStringBuilder
        {
            ConnectionString = connectionString
        };
    }

    public DbContextOptionsBuilder GetContextBuilder(DbContextOptionsBuilder optionsBuilder,
        DbConnectionSettings dbConnectionSettings)
    {
        return optionsBuilder.UseSqlServer(dbConnectionSettings.ConnectionString);
    }

    public DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(
        DbContextOptionsBuilder<TContext> contextOptionsBuilder,
        string connectionString) where TContext : DbContext
    {
        return contextOptionsBuilder.UseSqlServer(connectionString);
    }
}