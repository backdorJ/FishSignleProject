using FishShop.Core.Services;
using FishShop.Core.Services.GuidFactory;
using FishShop.Core.Services.JWTService;
using FishShop.Core.Services.NextFactory;
using FishShop.Core.Services.PasswordService;
using FishShop.Core.Services.UserClaimsManager;
using Microsoft.Extensions.DependencyInjection;

namespace FishShop.Core;

public static class Entry
{
    /// <summary>
    /// Добавить слой Core
    /// </summary>
    /// <param name="services">Сервисы</param>
    public static void AddCore(this IServiceCollection services)
    {
        services.AddMediatR(from => from.RegisterServicesFromAssembly(typeof(Entry).Assembly));
        services.AddTransient<DbSeeder>();

        services
            .AddScoped<IJwtGenerator, JwtGenerator>()
            .AddScoped<IUserMangerService, UserMangerService>()
            .AddScoped<IPasswordService, PasswordService>()
            .AddScoped<IGuidFactory, GuidFactory>()
            .AddScoped<INextFactory, NextFactory>();
    }
}