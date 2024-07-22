namespace FishShop.Core.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User : Entity
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userName">Логин пользователя</param>
    /// <param name="email">Почта</param>
    /// <param name="hashPassword">Хеш пароля</param>
    /// <param name="tempEmailCode">Код для почты</param>
    /// <param name="details">Детали</param>
    /// <param name="roles">Роли</param>
    public User(
        string userName,
        string email,
        string hashPassword,
        string? tempEmailCode = default,
        UserDetail? details = default,
        List<Role>? roles = default)
    {
        UserName = userName;
        Email = email;
        HashPassword = hashPassword;
        Details = details ?? new UserDetail();
        Roles = roles ?? new();
        TempEmailCode = tempEmailCode;
    }

    private User()
    {
    }

    /// <summary>
    /// Наименование логина
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Хеш пароля
    /// </summary>
    public string HashPassword { get; set; }

    /// <summary>
    /// Код подтверждения для почты
    /// </summary>
    public string? TempEmailCode { get; set; }
    
    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Данные пользователя
    /// </summary>
    public UserDetail Details { get; set; }

    /// <summary>
    /// Роли
    /// </summary>
    public List<Role> Roles { get; }

    /// <summary>
    /// Тестовая сущность
    /// </summary>
    /// <param name="email">Почта</param>
    /// <param name="code">Код</param>
    /// <param name="detail">Детали</param>
    /// <param name="roles">Роли</param>
    /// <param name="hashPassword">Хеш</param>
    /// <param name="userName">Имя</param>
    /// <param name="id">ИД</param>
    /// <returns></returns>
    [Obsolete("Только для тестов")]
    public static User CreateForTest(
        string? email = default,
        string? code = default,
        UserDetail? detail = default,
        List<Role>? roles = default,
        string? hashPassword = default,
        string? userName = default,
        Guid? id = default)
        => new(
            userName: userName ?? string.Empty,
            email: email ?? string.Empty,
            hashPassword: hashPassword ?? String.Empty,
            roles: roles,
            tempEmailCode: code)
            {
                Id = id ?? Guid.NewGuid(),
                Email = email ?? string.Empty,
                Details = detail ?? new()
            };
}