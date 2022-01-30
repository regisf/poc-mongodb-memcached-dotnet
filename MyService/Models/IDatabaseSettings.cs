namespace MyService.Models;

public interface IDatabaseSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string HelloCollection { get; set; }
}
