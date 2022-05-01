var builder = WebApplication.CreateBuilder(args);
var mongoSettings = builder.Configuration.GetSection("MongoConnection");
var authSettings = builder.Configuration.GetSection("AuthenticationSettings");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AuthenticationSettings>(authSettings);
builder.Services.Configure<DatabaseSettings>(mongoSettings);
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddTransient<IArticleRepository, ArticleRepository>();
builder.Services.AddTransient<IStoreService, StoreService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ArticleValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginValidator>());
var app = builder.Build();

/*OTHER*/
app.MapSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");


/*GET*/

app.MapGet("/articles", (IStoreService storeservice) => storeservice.GetArticles());
app.MapGet("/article/{id}", (IStoreService storeservice, string id) => storeservice.GetArticle(id));
app.MapGet("/users", (IStoreService storeservice) => storeservice.GetLogins());
app.MapGet("/user/{id}", (IStoreService storeservice, string id) => storeservice.GetLogin(id));
app.MapGet("/articles/category/{category}", (IStoreService storeservice, string category) => storeservice.GetArticlesByCategory(category));

/*POST*/

app.MapPost("/user", async (IStoreService storeservice, Login login, IValidator<Login> validator) =>
    {
        var result = validator.Validate(login);

        if (result.IsValid)
        {
            var created = await storeservice.AddLogin(login);
            return Results.Created($"/user/{created.Id}", created);
        }

        var errors = result.Errors.Select(e => new { errors = e.ErrorMessage });
        return Results.BadRequest(errors);
    });

app.MapPost("/article", async (Article article, IStoreService storeservice, IValidator<Article> validator) =>
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
app.MapPost("/auth", async (Login login, IAuthenticationService authService, IOptions<AuthenticationSettings> authsettings, IStoreService storeservice) =>
{

    var created = await storeservice.VerifyUserAsync(login.Email, login.Password);
    if (created == null)
    {
        return Results.BadRequest(new { errors = "Invalid credentials" });
    }
    var token = authService.GenerateToken(authsettings, created);
    return Results.Ok(new { id = created.Id, name = created.Name, email = created.Email, token });
});

app.Run();

//Hack om testen te doen werken
public partial class Program { }
