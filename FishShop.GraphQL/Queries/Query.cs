using FishShop.Core.Abstractions;
using FishShop.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FishShop.GraphQL.Queries;

/// <summary>
/// Запросы для GraphQl
/// </summary>
public class Query
{
    /// <summary>
    /// Получить товары
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <returns>Товары</returns>
    [UseProjection]
    public IQueryable<Product> Read([FromServices] IDbContext dbContext)
        => dbContext.Products;
}