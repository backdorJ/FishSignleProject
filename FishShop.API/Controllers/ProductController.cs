using FishShop.API.Versions;
using FishShop.Contracts.Requests.ProductsRequests.AddProduct;
using FishShop.Core.Requests.ProductsRequests.AddProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FishShop.API.Controllers;

/// <summary>
/// Контроллер "Товары"
/// </summary>
[ApiVersion(ApiVersions.V1)]
[Authorize]
public class ProductController : BaseApiController
{
    /// <summary>
    /// Создать сущность
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>ИД сущности</returns>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(AddProductResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
    public async Task<AddProductResponse> AddProductAsync(
        [FromServices] IMediator mediator,
        [FromBody] AddProductRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(
            new AddProductCommand
            {
                Name = request.Name,
                Price = request.Price,
                CategoryType = request.CategoryType
            }, cancellationToken);
}