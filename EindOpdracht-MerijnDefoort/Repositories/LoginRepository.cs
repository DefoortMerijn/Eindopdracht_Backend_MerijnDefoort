namespace Store.API.Repositories;
public interface ILoginRepository
{
    Task<Login> AddLogin(Login NewLogins);
    Task<Login> GetLogin(string id);
    Task<List<Login>> GetLogins();
    Task<Login> VerifyUserAsync(string email, string password);
}

public class LoginRepository : ILoginRepository
{
    private readonly IMongoContext _context;
    public LoginRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Login> AddLogin(Login NewLogin)
    {
        await _context.LoginCollection.InsertOneAsync(NewLogin);
        return NewLogin;
    }

    public async Task<Login> VerifyUserAsync(string email, string password)
    { 
        var filter = Builders<Login>.Filter.Eq("Email", email);
        var result = await _context.LoginCollection.Find(filter).FirstOrDefaultAsync();
        if (result == null)
        {
            return null;
        }
        if (result.Password == password)
        {
            return result;
        }
        return null;

    }
    public async Task<List<Login>> GetLogins() => await _context.LoginCollection.Find(_ => true).ToListAsync();

    public async Task<Login> GetLogin(string id) => await _context.LoginCollection.Find<Login>(id).FirstOrDefaultAsync();

}