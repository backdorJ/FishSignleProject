namespace FishShop.Core.Exceptions;

/// <summary>
/// Ошибка валидации
/// </summary>
public class ValidationException : ApplicationException
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="message">Сообщение с ошибкой</param>
    public ValidationException(string message)
        : base(message)
    {
    }
}