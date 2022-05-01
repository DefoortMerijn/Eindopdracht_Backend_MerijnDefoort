namespace StoreTests.FakeRepositories;

public class FakeLoginRepository : ILoginRepository
{
    public static List<Login> _logins = new List<Login>();
    public FakeLoginRepository()
    {
        _logins.Add(new Login()
        {
            Id = "1",
            Email = "test@testmail.com",
            Password = "test"
        });
    }
    public Task<Login> AddLogin(Login login)
    {
        _logins.Add(login);
        return Task.FromResult(login);
    }
    public Task<List<Login>> GetLogins()
    {
        return Task.FromResult(_logins);
    }
    public Task<Login> GetLogin(string id)
    {
        return Task.FromResult(_logins.FirstOrDefault(a => a.Id == id));
    }

    public Task<Login> VerifyUserAsync(string email, string password)
    {
        return Task.FromResult(_logins.FirstOrDefault(a => a.Email == email && a.Password == password));
    }


}