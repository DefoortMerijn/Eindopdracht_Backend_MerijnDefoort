namespace Store.GraphQL.Mutations;



public class Mutation
{

    public async Task<AddArticlePayload> AddArticle([Service] IStoreService storeService, AddArticleInput input)
    {
        var newArticle = new Article()
        {
            Name = input.Name,
            Description = input.Description,
            Price = input.Price,
            Image = input.Image
        };
        var created = await storeService.AddArticle(newArticle);
        return new AddArticlePayload(created);
    }

    public async Task<AddLoginPayload> AddLogin([Service] IStoreService storeService, AddLoginInput input)
    {
        var newLogin = new Login()
        {
            Name = input.Name,
            Password = input.Password,
            Email = input.Email
        };
        var created = await storeService.AddLogin(newLogin);
        return new AddLoginPayload(created);
    }


}