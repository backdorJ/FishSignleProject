using System.Security.Claims;
using FishShop.Core.Entities;

namespace FishShop.Core.Services.UserClaimsManager;

/// <summary>
/// Сервис для работы с claim
/// </summary>
public interface IUserMangerService
{
    /// <summary>
    /// Получить claims пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns>Список claims</returns>
    public List<Claim> GetUserClaims(User user);
}