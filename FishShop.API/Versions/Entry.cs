namespace FishShop.API.Versions;

/// <summary>
/// Добавление версионирования
/// </summary>
public static class Entry
{
    /// <summary>
    /// Версионирование конфигурация
    /// </summary>
    /// <param name="services">Сервисы</param>
    public static void AddCustomVersioning(this IServiceCollection services)
        => services.AddApiVersioning(conf =>
            {
                conf.AssumeDefaultVersionWhenUnspecified = true;
                conf.ReportApiVersions = true;
            })
            .AddVersionedApiExplorer(x =>
            {
                x.GroupNameFormat = "'v'VVV";
                x.SubstituteApiVersionInUrl = true;
            });
}