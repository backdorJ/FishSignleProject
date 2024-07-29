using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using FishShop.Core.Abstractions;

namespace FishShop.Core.Entities;

/// <summary>
/// Базовая сущность
/// </summary>
public class Entity
{
    /// <summary>
    /// Очередь событий
    /// </summary>
    [GraphQLIgnore]
    private ConcurrentQueue<IDomainEvent> _concurrentQueue = new();
    
    /// <summary>
    /// ИД сущности
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата создания сущности
    /// </summary>
    public DateTime CreateAt { get; set; }

    /// <summary>
    /// Дата обновления сущности
    /// </summary>
    public DateTime? UpdateAt { get; set; }

    /// <summary>
    /// Добавить событие в очередь
    /// </summary>
    /// <param name="domainEvent">Событие</param>
    [GraphQLIgnore]
    public void AddDomainEvent(IDomainEvent domainEvent) => _concurrentQueue.Enqueue(domainEvent);

    /// <summary>
    /// Получить события
    /// </summary>
    /// <returns>События</returns>
    [GraphQLIgnore]
    public IEnumerable<IDomainEvent> GetDomainEvents()
    {
        var result = _concurrentQueue
            .Select(x => x)
            .ToList();
        
        _concurrentQueue.Clear();

        return result;
    }
}