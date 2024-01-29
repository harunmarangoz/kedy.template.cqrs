using Api.Configurations;
using Domain.Settings;
using Infrastructure;
using Persistence;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, false);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddApiServices();
services.AddInfrastructure();
services.AddSharedServices();
services.AddPersistenceInfrastructure(configuration);
services.AddRedisConfiguration(configuration);
services.AddSwaggerConfiguration();
services.AddSerilogServices();

var app = builder.Build();

app.ConfigureApiConfiguration();
app.ConfigureSwaggerConfiguration();
app.ConfigurePersistenceInfrastructure();

app.UseHttpsRedirection();

await app.RunAsync();