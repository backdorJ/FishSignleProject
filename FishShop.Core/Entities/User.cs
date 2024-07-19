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
    /// <param name="details">Детали</param>
    /// <param name="roles">Роли</param>
    public User(
        string userName,
        string email,
        string hashPassword,
        UserDetail? details,
        List<Role>? roles)
    {
        UserName = userName;
        Email = email;
        HashPassword = hashPassword;
        Details = details ?? new UserDetail();
        Roles = roles ?? new();
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
}