using System.Data.Common;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Interfaces;

public interface IDbProvider
{
    IServiceCollection EnableDatabase(IServiceCollection services, DbConnectionSettings connectionStringSettings);

    DbContextOptionsBuilder GetContextBuilder(DbContextOptionsBuilder optionsBuilder,
        DbConnectionSettings connectionStringSettings);

    DbConnectionStringBuilder GetConnectionBuilder(string connectionString);

    DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(
        DbContextOptionsBuilder<TContext> contextOptionsBuilder, string connectionString) where TContext : DbContext;
}