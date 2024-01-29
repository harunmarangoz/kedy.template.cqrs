namespace Domain.Settings;

public class RedisSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }

    public string Prefix { get; set; } = "";
    public int DefaultExpireMinute { get; set; } = 60 * 24;
}