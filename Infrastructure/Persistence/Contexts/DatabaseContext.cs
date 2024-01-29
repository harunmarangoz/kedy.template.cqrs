using System.Globalization;
using Application.Services;
using Contracts.Constants;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Extensions;

namespace Persistence.Contexts;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }
    
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    public virtual DbSet<Form> Forms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.CustomizeConventions();
        modelBuilder.ApplyAllEntityConfigurations();
        modelBuilder.AddGlobalFilter();

        base.OnModelCreating(modelBuilder);
    }
}