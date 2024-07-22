namespace FishShop.Contracts.Requests.AuthRequests.ConfirmEmailCode;

/// <summary>
/// Запрос на подтверждение почты
/// </summary>
public class ConfirmEmailCodeRequest
{
    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; } = default!;
}