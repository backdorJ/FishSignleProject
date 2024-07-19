namespace FishShop.Contracts.Requests.AuthRequests.PostRegister;

/// <summary>
/// Модель для запроса <see cref="PostRegisterRequest"/>
/// </summary>
public class PostRegisterUserDetailRequest
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// День рождения пользователя
    /// </summary>
    public DateTime? BirthDate { get; set; }
}