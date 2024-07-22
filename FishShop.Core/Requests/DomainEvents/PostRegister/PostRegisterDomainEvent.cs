using FishShop.Core.Abstractions;
using FishShop.Core.Entities;

namespace FishShop.Core.Requests.DomainEvents.PostRegister;

/// <summary>
/// Событие на регистрацию
/// </summary>
public class PostRegisterDomainEvent : IDomainEvent
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="user">Пользователь</param>
    public PostRegisterDomainEvent(User user)
        => User = user;

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; }
}