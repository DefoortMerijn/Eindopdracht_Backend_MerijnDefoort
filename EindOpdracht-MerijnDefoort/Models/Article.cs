namespace Store.API.Models;

public class Article
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public int? salePercentage { get; set; }
    public string? Image { get; set; }
    public string? Category { get; set; }
    public Dictionary<string, string>? Specs { get; set; }
}