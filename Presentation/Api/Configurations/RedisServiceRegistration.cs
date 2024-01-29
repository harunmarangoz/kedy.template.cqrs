using Application.Services;
using Domain.Exceptions;
using Domain.Settings;
using Shared.Services;
using StackExchange.Redis;

namespace Api.Configurations;

public static class RedisServiceRegistration
{
    public static IServiceCollection AddRedisConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var redisSettings = configuration.GetRequiredSection("RedisSettings").Get<RedisSettings>();
        if (redisSettings is null) throw new AppException("Redis settings are not configured");

        var multiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions()
        {
            EndPoints = { $"{redisSettings.Host}:{redisSettings.Port}" },
            Password = redisSettings.Password
        });

        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        services.AddSingleton<ICacheService, RedisCacheService>();
        services.AddSingleton(redisSettings);

        return services;
    }
}