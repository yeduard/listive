using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Login.Api.Configurations;
using Login.Api.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Login.Api.Services;

public interface IJwtService
{
    AuthenticationResponse CreateToken(IdentityUser user);
}

public class JwtService : IJwtService
{
    private readonly JwtConfiguration _config;

    public JwtService(IConfiguration config)
    {
        this._config = config.GetSection("Jwt").Get<JwtConfiguration>();
    }

    public AuthenticationResponse CreateToken(IdentityUser user)
    {
        var expiration = DateTimeOffset.UtcNow.AddMinutes(_config.Expiration);

        var token = CreateJwtToken(CreateClaims(user), CreateSigningCredentials(), expiration);

        var tokenHandler = new JwtSecurityTokenHandler();

        return new AuthenticationResponse(tokenHandler.WriteToken(token), expiration);
    }

    private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTimeOffset expiration) =>
        new JwtSecurityToken(_config.Issuer, _config.Audience, claims, expires: expiration.DateTime, signingCredentials: credentials);

    private Claim[] CreateClaims(IdentityUser user) => new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, _config.Subject),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
    };

    private SigningCredentials CreateSigningCredentials() =>
        new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key)), SecurityAlgorithms.HmacSha256);
}