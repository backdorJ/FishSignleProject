using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FishShop.Core.Entities;
using FishShop.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FishShop.Core.Services.JWTService;

/// <summary>
/// Сервис для работы с jwt токеном
/// </summary>
public class JwtGenerator(IOptions<JwtOptions> options) : IJwtGenerator
{
    private readonly JwtOptions _options = options.Value;

    /// <inheritdoc />
    public string GenerateToken(List<Claim> claims)
    {
        var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));

        var jwt = new JwtSecurityToken(issuer: _options.Issuer,
            audience: _options.Audience,
            expires: DateTime.Now.AddMinutes(_options.LifeTime),
            claims: claims,
            signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}