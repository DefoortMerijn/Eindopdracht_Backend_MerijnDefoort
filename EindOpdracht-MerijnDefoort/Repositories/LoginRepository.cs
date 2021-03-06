namespace Store.API.Repositories;


public interface ILoginRepository
{
    Task<Login> AddLogin(Login NewLogins);
    Task<Login> GetLogin(string id);
    Task<List<Login>> GetLogins();

    Task<Login> GetLoginByEmailAsync(string email);
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

    public async Task<List<Login>> GetLogins() => await _context.LoginCollection.Find(_ => true).ToListAsync();
    public async Task<Login> GetLogin(string id) => await _context.LoginCollection.Find<Login>(Login => Login.Id == id).FirstOrDefaultAsync();

    public async Task<Login> GetLoginByEmailAsync(string email) => await _context.LoginCollection.Find<Login>(Login => Login.Email == email).FirstOrDefaultAsync();

}