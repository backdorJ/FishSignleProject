using System.Security.Claims;
using FishShop.Core.Entities;

namespace FishShop.Core.Services.UserClaimsManager;

/// <summary>
/// Сервис для работы с claims
/// </summary>
public class UserMangerService : IUserMangerService
{
    /// <inheritdoc />
    public List<Claim> GetUserClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
        };

        var userRoles = GetUserRoles(user);

        claims.AddRange(userRoles
            .Select(x => new Claim(ClaimTypes.Role, x)));

        return claims;
    }

    private List<string> GetUserRoles(User user)
        => user.Roles
            .Select(x => x.RoleName)
            .ToList();
}