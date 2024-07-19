namespace FishShop.Contracts.Requests.AuthRequests.PostLogin;

/// <summary>
/// Ответ для <see cref="PostLoginRequest"/>
/// </summary>
public class PostLoginResponse
{
    /// <summary>
    /// Токен
    /// </summary>
    public string AccessToken { get; set; } = default!;
}