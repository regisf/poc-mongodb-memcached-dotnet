using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyService.Models;

public class HelloModel : IHelloModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = null!;

    [BsonElement("guid")] public string? Uid { get; set; } = null!;

    [BsonElement("description")] public string? Description { get; set; } = null!;

    [BsonElement("name")] public string? Name { get; set; } = null!;

    [BsonElement("category")] public string? Category { get; set; } = null!;

    [BsonElement("ean")] public string? Ean { get; set; } = null!;
}
