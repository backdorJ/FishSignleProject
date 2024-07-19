namespace FishShop.Core.Exceptions;

/// <summary>
/// Обязательное поле ошибка
/// </summary>
public class RequiredException : ApplicationException
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="message">Сообщение об ошибки</param>
    public RequiredException(string message)
        : base(message)
    {
    }
}