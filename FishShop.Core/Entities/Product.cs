using FishShop.Contracts.Enums;

namespace FishShop.Core.Entities;

/// <summary>
/// Товар
/// </summary>
public class Product : Entity
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="name">Название</param>
    /// <param name="price">Цена</param>
    /// <param name="type">Тип товара</param>
    public Product(
        string name,
        decimal price,
        CategoryType type)
    {
        Name = name;
        Price = price;
        Type = type;
    }

    /// <summary>
    /// Название товара
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Цена товара
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Тип товара
    /// </summary>
    public CategoryType Type { get; set; }
}