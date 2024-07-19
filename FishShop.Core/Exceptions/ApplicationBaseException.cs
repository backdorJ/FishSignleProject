namespace FishShop.Core.Exceptions;

/// <summary>
/// Базовая ошибка
/// </summary>
public class ApplicationBaseException : Exception
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="message">Сообщение об ошибке</param>
    public ApplicationBaseException(string message)
        : base(message)
    {
    }
}