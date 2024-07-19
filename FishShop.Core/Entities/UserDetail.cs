namespace FishShop.Core.Entities;

/// <summary>
/// Данные пользователя
/// </summary>
public class UserDetail
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