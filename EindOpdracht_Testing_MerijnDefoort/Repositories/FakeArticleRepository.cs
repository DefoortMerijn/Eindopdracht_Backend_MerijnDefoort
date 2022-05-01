namespace StoreTests.FakeRepositories;


public class FakeArticleRepository : IArticleRepository
{
    public static List<Article> _articles = new List<Article>();

    public FakeArticleRepository()
    {
        _articles.Add(new Article()
        {
            Id = "1",
            Name = "Phone",
            Price = 100,
            Category = "Electronics",
            Description = "A phone",
            Image = "http://www.google.com",
            salePercentage = 0,
        });
    }

    public Task<Article> AddArticle(Article article)
    {
        _articles.Add(article);
        return Task.FromResult(article);
    }

    public static Task<Article> AddFakeArticle(Article article)
    {

        _articles.Add(article);
        return Task.FromResult(article);
    }

    public Task<List<Article>> AddArticles(List<Article> articles)
    {
        _articles.AddRange(articles);
        return Task.FromResult(articles);
    }
    public Task<List<Article>> GetArticles()
    {
        return Task.FromResult(_articles);
    }
    public Task<Article> GetArticle(string id)
    {
        return Task.FromResult(_articles.FirstOrDefault(a => a.Id == id));
    }
    public Task<List<Article>> GetArticlesByCategory(string category)
    {
        return Task.FromResult(_articles.Where(a => a.Category == category).ToList());
    }


}