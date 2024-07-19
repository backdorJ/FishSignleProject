using FishShop.Contracts.Requests.ProductsRequests.AddProduct;
using FishShop.Core.Abstractions;
using FishShop.Core.Entities;
using FishShop.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishShop.Core.Requests.ProductsRequests.AddProduct;

/// <summary>
/// Обработчик для <see cref="AddProductCommand"/>
/// </summary>
public class AddProductCommandHandler
    : IRequestHandler<AddProductCommand, AddProductResponse>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public AddProductCommandHandler(IDbContext dbContext)
        => _dbContext = dbContext;

    /// <inheritdoc />
    public async Task<AddProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var isExistInDb = await _dbContext.Products
            .AnyAsync(x =>
                    x.Name.ToLower() == request.Name.ToLower()
                    && x.Type == request.CategoryType,
                cancellationToken);

        if (isExistInDb)
            throw new ValidationException("Такой товар присутствует!");

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new RequiredException("Поле 'Наименование товара' является обязательным");

        if (request.Price <= 0)
            throw new RequiredException("Поле 'Цена' является обязательным");

        var product = new Product(
            request.Name,
            request.Price,
            request.CategoryType);

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AddProductResponse(product.Id);
    }
}