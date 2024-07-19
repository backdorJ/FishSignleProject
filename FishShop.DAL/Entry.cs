using FishShop.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FishShop.DAL;

public static class Entry
{
    public static void AddDAL(this IServiceCollection services)
    {
        services.AddScoped<IDbContext, AppDbContext>();
        services.AddTransient<Migrator>();

        services.AddLogging();
    }
}