namespace Store.API.Configuration;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? LoginCollection { get; set; }
    public string? ArticleCollection { get; set; }

}