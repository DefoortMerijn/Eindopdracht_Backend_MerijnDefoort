var builder = WebApplication.CreateBuilder(args);
var mongoSettings = builder.Configuration.GetSection("MongoConnection");

builder.Services.Configure<DatabaseSettings>(mongoSettings);
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddTransient<IArticleRepository, ArticleRepository>();
builder.Services.AddTransient<IStoreService, StoreService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ArticleValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginValidator>());
var app = builder.Build();

/*OTHER*/

app.MapGet("/", () => "Hello World!");


/*GET*/

app.MapGet("/articles", (IStoreService storeservice) => storeservice.GetArticles());
app.MapGet("/article/{id}", (IStoreService storeservice, string id) => storeservice.GetArticle(id));
app.MapGet("/users", (IStoreService storeservice) => storeservice.GetLogins());
app.MapGet("/user/{id}", (IStoreService storeservice, string id) => storeservice.GetLogin(id));

/*POST*/

app.MapPost("/user", async (IStoreService storeservice, Login login, IValidator<Login> validator) =>
    {
        var result = validator.Validate(login);

        if (result.IsValid)
        {
            var created = await storeservice.AddLogin(login);
            return Results.Created($"/login/{created.Id}", created);
        }

        var errors = result.Errors.Select(e => new { errors = e.ErrorMessage });
        return Results.BadRequest(errors);
    });

app.MapPost("/article", async (IStoreService storeservice, IValidator<Article> validator, Article article) =>
{

    var result = validator.Validate(article);

    if (result.IsValid)
    {
        var created = await storeservice.AddArticle(article);
        return Results.Created($"/article/{created.Id}", created);
    }

    var errors = result.Errors.Select(e => new { errors = e.ErrorMessage });
    return Results.BadRequest(errors);
});

app.MapPost("/articles", async (List<Article> articles, IStoreService storeservice, IValidator<Article> validator) =>
{

    var created = await storeservice.AddArticles(articles);
    foreach (var article in created)
    {
        var result = validator.Validate(article);

        if (!result.IsValid) return Results.BadRequest(result.Errors.Select(e => new { errors = e.ErrorMessage }));
        article.Id = $"/article/{article.Id}";
        return Results.Created(article.Id, created);
    }
    return Results.StatusCode(500);
});


app.Run();
