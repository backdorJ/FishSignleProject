namespace FishShop.Contracts.Requests.AuthRequests.PostRegister;

/// <summary>
/// Запрос на регистрацию в системе
/// </summary>
public class PostRegisterRequest
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Username { get; set; } = default!;

    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Детали пользователя
    /// </summary>
    public PostRegisterUserDetailRequest UserDetails { get; set; } = default!;
}