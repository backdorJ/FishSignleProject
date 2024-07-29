using FishShop.GraphQL.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FishShop.GraphQL;

/// <summary>
/// Входная точка для подключения зависимостей для GraphQl
/// </summary>
public static class Entry
{
    /// <summary>
    /// Добавить конечную точку GraphQl
    /// </summary>
    /// <param name="app"></param>
    public static void MapMyGraphQl(this WebApplication app)
    {
        app.MapGraphQLHttp();
        app.MapBananaCakePop();
    }

    /// <summary>
    /// Добавить сервис GraphQl
    /// </summary>
    /// <param name="services"></param>
    public static void AddMyGraphQl(this IServiceCollection services)
        => services.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddAuthorization()
            .AddProjections()
            .AddFiltering()
            .AddSorting();
}