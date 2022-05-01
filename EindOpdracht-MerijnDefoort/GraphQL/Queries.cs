namespace Store.GraphQL.Queries;

public class Queries
{
    public async Task<List<Article>> GetArticles([Service] IStoreService storeservice) => await storeservice.GetArticles();
    public async Task<Article> GetArticle([Service] IStoreService storeservice, string id) => await storeservice.GetArticle(id);
    public async Task<Login> GetLogin([Service] IStoreService storeservice, string id) => await storeservice.GetLogin(id);
    public async Task<List<Login>> GetLogins([Service] IStoreService storeservice) => await storeservice.GetLogins();
}