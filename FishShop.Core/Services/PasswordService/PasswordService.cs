namespace FishShop.Core.Services.PasswordService;

/// <summary>
/// Сервис для хеширования паролей
/// </summary>
public class PasswordService : IPasswordService
{
    /// <inheritdoc />
    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    /// <inheritdoc />
    public bool VerifyPassword(string password, string hashPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashPassword);
}