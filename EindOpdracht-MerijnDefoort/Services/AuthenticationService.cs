namespace Store.API.Services;

public record AuthenticationRequestBody()
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public interface IAuthenticationService
{
    string GenerateToken(IOptions<AuthenticationSettings> authSettings, Login login);
    Task<Login> ValidateUser(string email, string password);
}

public class AuthenticationService : IAuthenticationService
{
    readonly ILoginRepository _loginRepository;
    public AuthenticationService(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }
    public async Task<Login> ValidateUser(string email, string password)
    {
        var user = await _loginRepository.GetLoginByEmailAsync(email);
        return BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
    }
    public string GenerateToken(IOptions<AuthenticationSettings> authSettings, Login login)
    {
        // TOKEN AANMAKEN
        // symmetric key maken
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.Value.SecretKey));
        // credentials om token te signen
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // claims aanmaken
        var claims = new List<Claim>
  {
    new Claim("id", "1"),
    new Claim ("userID", login.Id),
    new Claim(JwtRegisteredClaimNames.Sub, login.Name),
    new Claim(JwtRegisteredClaimNames.Email, login.Email),
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
  };
        var jwtToken = new JwtSecurityToken(
          issuer: authSettings.Value.Issuer,
          audience: authSettings.Value.Audience,
          claims: claims,
          expires: DateTime.Now.AddDays(1),
          signingCredentials: signingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}