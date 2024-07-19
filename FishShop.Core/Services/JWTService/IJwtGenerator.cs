using System.Security.Claims;

namespace FishShop.Core.Services.JWTService;

/// <summary>
/// Сервис для работы с jwt токеном
/// </summary>
public interface IJwtGenerator
{
    /// <summary>
    /// Сгенерировать токен
    /// </summary>
    /// <returns>Токен</returns>
    public string GenerateToken(List<Claim> claims);
}