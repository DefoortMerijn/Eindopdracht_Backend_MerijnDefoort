namespace Store.API.Services;


public interface IStoreService
{
    Task<Article> AddArticle(Article NewArticle);
    Task<List<Article>> AddArticles(List<Article> NewArticles);
    Task<Login> AddLogin(Login NewLogin);
    Task<Article> GetArticle(string id);
    Task<List<Article>> GetArticles();
    Task<List<Article>> GetArticlesByCategory(string category);
    Task<Login> GetLogin(string id);
    Task<List<Login>> GetLogins();
}

public class StoreService : IStoreService
{
    private readonly ILoginRepository _loginRepository;
    private readonly IArticleRepository _articleRepository;
    public StoreService(ILoginRepository loginRepository, IArticleRepository articleRepository)
    {
        _loginRepository = loginRepository;
        _articleRepository = articleRepository;
    }
    public async Task<List<Article>> GetArticles() => await _articleRepository.GetArticles();
    public async Task<Login> GetLogin(string id) => await _loginRepository.GetLogin(id);
    public async Task<List<Login>> GetLogins() => await _loginRepository.GetLogins();
    public async Task<Login> AddLogin(Login NewLogin) => await _loginRepository.AddLogin(NewLogin);
    public async Task<Article> AddArticle(Article NewArticle) => await _articleRepository.AddArticle(NewArticle);
    public async Task<List<Article>> AddArticles(List<Article> NewArticles) => await _articleRepository.AddArticles(NewArticles);
    public async Task<Article> GetArticle(string id) => await _articleRepository.GetArticle(id);
    public async Task<List<Article>> GetArticlesByCategory(string category) => await _articleRepository.GetArticlesByCategory(category);
}