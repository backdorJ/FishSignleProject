namespace FishShop.Contracts.Requests.ProductsRequests.AddProduct;

/// <summary>
/// Ответ на <see cref="AddProductRequest"/>
/// </summary>
public class AddProductResponse(Guid id)
{
    /// <summary>
    /// Ид созданной сущности
    /// </summary>
    public Guid Id { get; set; } = id;
}