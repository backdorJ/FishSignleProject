namespace FishShop.Contracts.Requests.AuthRequests.PostLogin;

/// <summary>
/// Запрос на логин
/// </summary>
public class PostLoginRequest
{
    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = default!;
}