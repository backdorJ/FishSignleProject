using System.Text.Json;
using FishShop.Contracts.Models;
using RabbitMQ.Client;

namespace FishShop.RabbitMQ;

public class Publisher : IPublisher, IDisposable
{
    private const string ExchangeName = "fish-shop-exchange";

    private readonly IConnection _connection;
    private readonly IModel _channel;

    public Publisher()
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "user",
            Password = "password",

        };
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    /// <inheritdoc />
    public void Send(QueueRequest notification)
    {
        var byteBody = JsonSerializer.SerializeToUtf8Bytes(notification);
        
        _channel.ExchangeDeclare(
            exchange: ExchangeName,
            type: ExchangeType.Topic);

        _channel.QueueDeclare(
            queue: notification.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false);
        
        _channel.QueueBindNoWait(
            queue: notification.QueueName,
            exchange: ExchangeName,
            routingKey: notification.RoutingKey,
            arguments: null);
        
        _channel.BasicPublish(
            exchange: ExchangeName,
            routingKey: notification.RoutingKey,
            basicProperties: null,
            body: byteBody);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _connection.Dispose();
        _channel.Dispose();
    }
}