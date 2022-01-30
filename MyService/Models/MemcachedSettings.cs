namespace MyService.Models;

public class MemcachedSettings : IMemcachedSettings
{
    public int Port { get; set; }
    public string Server { get; set; }
}