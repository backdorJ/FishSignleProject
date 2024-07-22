namespace FishShop.API.Cors;

/// <summary>
/// Входная точка для настройки CORS
/// </summary>
public static class Entry
{
    /// <summary>
    /// Добавить Cors
    /// </summary>
    /// <param name="services"></param>
    public static void AddCustomCors(this IServiceCollection services)
        => services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
            });
        });
}