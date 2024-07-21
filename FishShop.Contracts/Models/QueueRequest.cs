namespace FishShop.Contracts.Models;

public class QueueRequest
{
    /// <summary>
    /// Сообщение в виде словаря
    /// </summary>
    public Dictionary<string, string> Message { get; set; }

    /// <summary>
    /// Ключ для маршрутизации
    /// </summary>
    public string RoutingKey { get; set; }

    /// <summary>
    /// Название очереди
    /// </summary>
    public string QueueName { get; set; }
}