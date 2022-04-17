namespace Store.API.Repositories;
public interface IArticleRepository
{
    Task<Article> AddArticle(Article NewArticles);
    Task<List<Article>> AddArticles(List<Article> NewArticles);
    Task<Article> GetArticle(string id);
    Task<List<Article>> GetArticles();
}

public class ArticleRepository : IArticleRepository
{
    private readonly IMongoContext _context;
    public ArticleRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Article> AddArticle(Article NewArticle)
    {
        await _context.ArticleCollection.InsertOneAsync(NewArticle);
        return NewArticle;
    }

    public async Task<List<Article>> AddArticles(List<Article> NewArticles)
    {
        await _context.ArticleCollection.InsertManyAsync(NewArticles);
        return NewArticles;
    }
    public async Task<List<Article>> GetArticles() => await _context.ArticleCollection.Find(_ => true).ToListAsync();

    public async Task<Article> GetArticle(string id) => await _context.ArticleCollection.Find<Article>(id).FirstOrDefaultAsync();

}