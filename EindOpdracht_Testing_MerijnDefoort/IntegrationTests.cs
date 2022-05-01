namespace StoreTests.Testing;

public class IntegrationTests
{
    [Fact]
    public async Task Should_Return_Articles()
    {
        var application = ApiHelper.CreateApi();
        var client = application.CreateClient();
        var result = await client.GetAsync("/articles");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var articles = await result.Content.ReadFromJsonAsync<List<Article>>();
        Assert.NotNull(articles);
        Assert.True(articles.Count > 0);
        Assert.IsType<List<Article>>(articles);

    }
    [Fact]
    public async Task Should_Return_Users()
    {
        
        var application = ApiHelper.CreateApi();
        var client = application.CreateClient();
        var result = await client.GetAsync("/users");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var users = await result.Content.ReadFromJsonAsync<List<Login>>();
        Assert.NotNull(users);
        Assert.True(users.Count > 0);
        Assert.IsType<List<Login>>(users);

    }
    [Fact]
    public async Task Should_Return_ArticleById()
    {
        var application = ApiHelper.CreateApi();
        var client = application.CreateClient();
        var result = await client.GetAsync("/article/1");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var article = await result.Content.ReadFromJsonAsync<Article>();
        Assert.NotNull(article);
        Assert.IsType<Article>(article);
        Assert.Equal("1", article.Id);
    }

    [Fact]
    public async Task Add_Article_Created()
    {
        var application = ApiHelper.CreateApi();
        var client = application.CreateClient();
        await FakeArticleRepository.AddFakeArticle(new Article()
        {
            Id = "2",
            Name = "Phone",
            Price = 100,
            Category = "Electronics",
            Description = "A phone",
            Image = "http://www.google.com",
            salePercentage = 0,
        });
        var result = await client.GetAsync("/article/2");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var article = await result.Content.ReadFromJsonAsync<Article>();
        Assert.NotNull(article);
        Assert.Equal("2", article.Id);


    }
}