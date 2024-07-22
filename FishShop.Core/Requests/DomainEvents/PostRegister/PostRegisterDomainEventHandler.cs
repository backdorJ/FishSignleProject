using FishShop.Contracts.Models;
using FishShop.Core.Abstractions;
using FishShop.Core.Exceptions;
using MediatR;
using IPublisher = FishShop.RabbitMQ.IPublisher;

namespace FishShop.Core.Requests.DomainEvents.PostRegister;

/// <summary>
/// Событие для <see cref="PostRegisterDomainEvent"/>
/// </summary>
public class PostRegisterDomainEventHandler : INotificationHandler<PostRegisterDomainEvent>
{
    private const string QueueRouteKey = "email-codes";
    private const string QueueName = "email-codes";
    
    private readonly IPublisher _publisher;
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="publisher">Publisher RabbitMQ</param>
    /// <param name="dbContext">Контекст БД</param>
    public PostRegisterDomainEventHandler(IPublisher publisher, IDbContext dbContext)
    {
        _publisher = publisher;
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public Task Handle(PostRegisterDomainEvent notification, CancellationToken cancellationToken)
    {
        _publisher.Send(new QueueRequest
        {
            Message = new Dictionary<string, string>
            {
                ["Code"] = notification.User.TempEmailCode
                           ?? throw new ApplicationBaseException("Код не был присвоен пользователю"),
                ["Email"] = notification.User.Email
            },
            RoutingKey = QueueRouteKey,
            QueueName = QueueName,
        });

        return Task.CompletedTask;
    }
}