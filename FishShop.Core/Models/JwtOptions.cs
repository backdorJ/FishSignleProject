namespace FishShop.Core.Models;

/// <summary>
/// Настройки для JWT
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Мы
    /// </summary>
    public string Issuer { get; set; } = default!;

    /// <summary>
    /// Кому
    /// </summary>
    public string Audience { get; set; } = default!;

    /// <summary>
    /// Секретный ключ
    /// </summary>
    public string Secret { get; set; } = default!;

    /// <summary>
    /// Время жизни
    /// </summary>
    public long LifeTime { get; set; }
}