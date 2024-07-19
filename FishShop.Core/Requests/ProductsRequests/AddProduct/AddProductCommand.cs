using FishShop.Contracts.Requests.ProductsRequests.AddProduct;
using MediatR;

namespace FishShop.Core.Requests.ProductsRequests.AddProduct;

/// <summary>
/// Команда на создание товара
/// </summary>
public class AddProductCommand : AddProductRequest, IRequest<AddProductResponse>
{
}