namespace Application.Services;

public interface ICacheService
{
    Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func, TimeSpan? timeToLive = null);
    Task<T> GetOrSetAsync<T>(string key, Func<T> func, TimeSpan? timeToLive = null);
    Task<T> GetOrSetAsync<T>(string key, T value, TimeSpan? timeToLive = null);
    Task<T> GetAsync<T>(string key);
    Task<string> GetAsync(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? timeToLive = null);
    Task SetStringAsync(string key, string value, TimeSpan? timeToLive = null);
    Task RemoveAsync(string key);
    Task RemoveByPrefixAsync(string prefix);
    Task<bool> IsSetAsync(string key);
    Task<IEnumerable<string>> GetKeysAsync(string prefix);
    Task<int> CountAsync();
    Task ClearAsync();
}