using FishShop.Contracts.Enums;

namespace FishShop.Contracts.Requests.ProductsRequests.AddProduct;

/// <summary>
/// Запрос на создание товара
/// </summary>
public class AddProductRequest
{
    /// <summary>
    /// Название товара
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Тип товара
    /// </summary>
    public CategoryType CategoryType { get; set; }
}