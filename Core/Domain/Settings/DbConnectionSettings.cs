namespace Domain.Settings;

public class DbConnectionSettings
{
    public string ConnectionString { get; set; } = default!;
    public string ProviderType { get; set; } = default!;
}