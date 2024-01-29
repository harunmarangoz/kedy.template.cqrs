using System.Text.Json;
using Application.Services;
using Domain.Settings;
using StackExchange.Redis;

namespace Shared.Services;

public class RedisCacheService(IConnectionMultiplexer redisCon, RedisSettings redisSettings) : ICacheService
{
    private readonly IDatabase _cache = redisCon.GetDatabase();

    #region GetOrSet

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeToLive = null)
    {
        var result = await GetAsync(key);
        if (result != null)
            return JsonSerializer.Deserialize<T>(result);

        var funcResult = await func();
        await SetAsync(key, funcResult, timeToLive);
        return funcResult;
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<T> func, TimeSpan? timeToLive = null)
    {
        var result = await GetAsync(key);
        if (result != null)
            return JsonSerializer.Deserialize<T>(result);

        var funcResult = func();
        await SetAsync(key, funcResult, timeToLive);
        return funcResult;
    }

    public async Task<T> GetOrSetAsync<T>(string key, T value, TimeSpan? timeToLive = null)
    {
        var result = await GetAsync(key);
        if (result != null)
            return JsonSerializer.Deserialize<T>(result);

        await SetAsync(key, value);
        return value;
    }

    #endregion

    #region GetAsync

    public async Task<T> GetAsync<T>(string key)
    {
        var stringJson = await GetAsync(key);
        return stringJson == null
            ? default
            : JsonSerializer.Deserialize<T>(stringJson);
    }

    public async Task<string> GetAsync(string key)
    {
        var result = await _cache.StringGetAsync(GetKey(key));
        return result.IsNull ? null : result.ToString();
    }

    #endregion

    #region SetAsync

    public async Task SetAsync<T>(string key, T value, TimeSpan? timeToLive = null)
    {
        await _cache.StringSetAsync(GetKey(key), JsonSerializer.Serialize(value), timeToLive ?? TimeSpan.FromDays(1));
    }

    public async Task SetStringAsync(string key, string value, TimeSpan? timeToLive = null)
    {
        await _cache.StringSetAsync(GetKey(key), value, timeToLive);
    }

    #endregion

    #region RemoveAsync

    public async Task RemoveAsync(string key)
    {
        await _cache.KeyDeleteAsync(key);
    }

    public async Task RemoveByPrefixAsync(string prefix)
    {
        var endpoints = redisCon.GetEndPoints(true);
        foreach (var endpoint in endpoints)
        {
            var server = redisCon.GetServer(endpoint);
            var keys = server.Keys(pattern: $"{prefix}*");
            foreach (var key in keys)
            {
                await _cache.KeyDeleteAsync(key);
            }
        }
    }

    #endregion

    public async Task<bool> IsSetAsync(string key)
    {
        return await _cache.KeyExistsAsync(GetKey(key));
    }

    public Task<IEnumerable<string>> GetKeysAsync(string prefix)
    {
        var endpoints = redisCon.GetEndPoints(true);
        var keys = new List<string>();
        foreach (var endpoint in endpoints)
        {
            var server = redisCon.GetServer(endpoint);
            keys.AddRange(server.Keys(pattern: GetKey($"{prefix}*")).Select(x => x.ToString()));
        }

        return Task.FromResult<IEnumerable<string>>(keys);
    }

    public Task<int> CountAsync()
    {
        var endpoints = redisCon.GetEndPoints(true);

        return Task.FromResult(endpoints.Select(endpoint => redisCon.GetServer(endpoint))
            .Select(server => (int)server.DatabaseSize(_cache.Database)).Sum());
    }

    public async Task ClearAsync()
    {
        var endpoints = redisCon.GetEndPoints(true);
        foreach (var endpoint in endpoints)
        {
            var server = redisCon.GetServer(endpoint);
            await server.FlushAllDatabasesAsync();
        }
    }

    private string GetKey(string key) =>
        string.IsNullOrEmpty(redisSettings.Prefix)
            ? key
            : $"{redisSettings.Prefix}:{key}";
}