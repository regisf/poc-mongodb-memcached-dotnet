namespace MyService.Models;

public class DatabaseSettings : IDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string HelloCollection { get; set; } = null!;
}