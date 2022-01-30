namespace MyService.Models;

public interface IMemcachedSettings
{
    int Port { get; set; }
    string Server { get; set; }
}