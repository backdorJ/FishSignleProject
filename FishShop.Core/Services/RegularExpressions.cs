using System.Text.RegularExpressions;

namespace FishShop.Core.Services;

/// <summary>
/// Класс с проверками на регулярные выражения
/// </summary>
public abstract class RegularExpressions
{
    /// <summary>
    /// Regex для почты
    /// </summary>
    private const string EmailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

    /// <summary>
    /// Проверить почту на корректность
    /// </summary>
    /// <param name="email">Почта</param>
    /// <returns>Корректная почта</returns>
    public static bool IsValidEmail(string email)
        => Regex.IsMatch(email, EmailRegex);
}