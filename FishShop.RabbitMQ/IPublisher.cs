using FishShop.Contracts.Models;

namespace FishShop.RabbitMQ;

/// <summary>
/// Отправитель в RabbitMQ
/// </summary>
public interface IPublisher
{
    /// <summary>
    /// Отравить сообщение в очередь о том, что пользователь зарегистрировался
    /// </summary>
    /// <param name="notification">Параметры для отправки</param>
    /// <returns>-</returns>
    public void Send(QueueRequest notification);
}