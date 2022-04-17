namespace Store.API.Context;

public interface IMongoContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Login> LoginCollection { get; }
    IMongoCollection<Article> ArticleCollection { get; }

}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Login> LoginCollection
    {
        get
        {
            return _database.GetCollection<Login>(_settings.LoginCollection);
        }
    }

    public IMongoCollection<Article> ArticleCollection
    {
        get
        {
            return _database.GetCollection<Article>(_settings.ArticleCollection);
        }
    }

}