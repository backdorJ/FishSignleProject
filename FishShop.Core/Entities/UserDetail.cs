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

    /// <summary>
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="firstName">Имя</param>
    /// <param name="lastName">Фамилия</param>
    /// <param name="patronymic">Отчество</param>
    /// <param name="birthDay">День рождения</param>
    /// <returns></returns>
    [Obsolete("Только для тестов")]
    public static UserDetail CreateForTest(
        string firstName,
        string lastName,
        string? patronymic = default,
        DateTime? birthDay = default)
        => new()
        {
            FirstName = firstName,
            LastName = lastName,
            Patronymic = patronymic,
            BirthDate = birthDay
        };
}