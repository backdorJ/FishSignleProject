using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FishShop.DAL;

/// <summary>
/// Класс накатывающий миграции
/// </summary>
public class Migrator
{
    private readonly ILogger _logger;
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="logger">Логгер</param>
    /// <param name="dbContext">Контекст БД</param>
    public Migrator(ILogger<Migrator> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Произвести миграцию
    /// </summary>
    public async Task MigrateAsync()
    {
        try
        {
            _logger.LogInformation("Начало обновления базы данных");
            await _dbContext.Database.MigrateAsync();
            _logger.LogInformation("Конец обновления базы данных");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);
        }
    }
}