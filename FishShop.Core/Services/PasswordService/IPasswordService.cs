namespace FishShop.Core.Services.PasswordService;

/// <summary>
/// Сервис для хеширования паролей
/// </summary>
public interface IPasswordService
{
    /// <summary>
    /// Хешировать пароль 
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <returns>Хеш</returns>
    public string HashPassword(string password);

    /// <summary>
    /// Проверить пароль с хешем
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="hashPassword">Хеш</param>
    /// <returns>Совпадают ли</returns>
    public bool VerifyPassword(string password, string hashPassword);
}