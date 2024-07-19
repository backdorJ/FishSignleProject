namespace FishShop.Core.Entities;

/// <summary>
/// Базовая сущность
/// </summary>
public class Entity
{
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
}