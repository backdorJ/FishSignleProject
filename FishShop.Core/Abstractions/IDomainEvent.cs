using MediatR;

namespace FishShop.Core.Abstractions;

/// <summary>
/// Доменное событие
/// </summary>
public interface IDomainEvent : INotification
{
}