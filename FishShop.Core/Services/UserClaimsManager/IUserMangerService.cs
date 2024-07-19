using System.Security.Claims;
using FishShop.Core.Entities;

namespace FishShop.Core.Services.UserClaimsManager;

/// <summary>
/// Сервис для работы с claim
/// </summary>
public interface IUserMangerService
{
    public List<Claim> GetUserClaims(User user);

    public List<string> GetUserRoles(User user);
}