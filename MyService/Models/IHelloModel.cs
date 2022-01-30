namespace MyService.Models;

public interface IHelloModel
{
    string? Id { get; set; }

    string? Uid { get; set; }

    string? Description { get; set; }

    string? Name { get; set; }

    string? Category { get; set; }

    string Ean { get; set; }
}